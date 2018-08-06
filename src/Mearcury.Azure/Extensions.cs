using Mearcury.Azure.Billing;
using Mearcury.Azure.ResourceManager;
using Mearcury.Core;
using System;
using System.Threading.Tasks;

namespace Mearcury.Azure
{
    public static class Extensions
    {
        public static async Task FillInExistingResourcesAsync(this AzureClient client, Resources resources)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Console.WriteLine("Azure resources are being loaded...");

            await client.Management.FillInExistingResourcesAsync(resources);

            Console.WriteLine("Azure resource load completed!");
        }

        public static async Task UpdateBillingResourcesAsync(this AzureClient client, Resources resources, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime))
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Console.WriteLine("Billing for Azure resources are being loaded...");

            await client.Billing.UpdateBillingResourcesAsync(resources, startDate, endDate);

            Console.WriteLine("Billing for Azure resource load completed!");
        }
    }
}
