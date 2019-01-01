using Mearcury.Core;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;
using System.Threading.Tasks;

namespace Mearcury.Azure.ResourceManager
{
    public static class Extensions
    {
        public static Mearcury.Core.Resource Convert(this IGenericResource resource)
        {
            return new Mearcury.Core.Resource(resource.Id, resource.Name, resource.ResourceGroupName, resource.Type, resource.Tags);
        }

        public static async Task FillInExistingResourcesAsync(this IAzure client, Resources resources)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            Type managementType = client.GetType();
            System.Reflection.FieldInfo resourceManagerField = managementType.GetField("resourceManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var resourceManager = (IResourceManager)resourceManagerField.GetValue(client);

            var azureResources = await resourceManager.GenericResources.ListAsync(true);

            foreach (var azureResource in azureResources)
                resources.Add(azureResource.Convert());
        }
    }
}
