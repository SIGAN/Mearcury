using CodeHollow.AzureBillingApi;
using Mearcury.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mearcury.Azure
{
    public static class Extensions
    {
        public static string Limit(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return source.Length < length ? source : source.Substring(0, length);
        }

        public static string Ellipsis(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return source.Length < length ? source : (source.Substring(0, length - 3) + "...");
        }

        public static Core.Resource Convert(this IGenericResource resource)
        {
            return new Core.Resource(resource.Id, resource.Name, resource.ResourceGroupName, resource.Type);
        }

        public static async Task FillInExistingResources(this AzureClient client, Resources resources)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            Type managementType = client.Management.GetType();
            System.Reflection.FieldInfo resourceManagerField = managementType.GetField("resourceManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var resourceManager = (IResourceManager)resourceManagerField.GetValue(client.Management);

            Console.WriteLine("Azure resources are being loaded...");

            var azureResources = await resourceManager.GenericResources.ListAsync(true);

            foreach (var azureResource in azureResources)
                resources.Add(azureResource.Convert());

            Console.WriteLine("Azure resource load completed!");
        }

        public static async Task GetBillingResources(this AzureClient client, Resources resources, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime))
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            if (startDate == default(DateTime))
                startDate = DateTime.Now.Subtract(TimeSpan.FromHours(2));

            if (endDate == default(DateTime))
                endDate = DateTime.Now.Subtract(TimeSpan.FromHours(1));

            var subscription = client.Subscription;

            Console.WriteLine("Billing for Azure resources are being loaded...");

            var rateCardData = client.Billing.GetRateCardData(
                    subscription.OfferId,
                    subscription.Currency,
                    subscription.Locale,
                    subscription.Region,
                    await client.GetManagementTokenAsync()
                );

            var usageData =
                client.Billing.GetUsageData(
                    startDate,
                    endDate,
                    CodeHollow.AzureBillingApi.Usage.AggregationGranularity.Hourly,
                    true,
                    await client.GetManagementTokenAsync()
                );

            var resourceCostData =
                Client.Combine(rateCardData, usageData);

            foreach (var cost in resourceCostData.Costs.GetCostsByResourceName())
            {
                var resourceName = cost.Key;
                var resourceCost = cost.Value;
                var totalCost = resourceCost.GetTotalCosts();

                if (resources.UpdateCostByName(resourceName, totalCost))
                    continue;

                var resourceId =
                    resourceCost.Select(item => item.UsageValue?.Properties?.InstanceData?.MicrosoftResources?.ResourceUri).FirstOrDefault(uri => uri != null)
                    ?? Guid.NewGuid().ToString();

                resources.Add(Core.Resource.DeletedResourceFromName(resourceId, resourceName));

                resources.UpdateCostByName(resourceName, totalCost);
            }

            Console.WriteLine("Billing for Azure resource load completed!");
        }
    }
}
