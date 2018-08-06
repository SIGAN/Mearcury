using Mearcury.Admin.Core;
using Mearcury.Azure;
using Mearcury.Core;
using System;
using System.Linq;

namespace Mearcury.Admin.Load
{
    public static class LoadCmd
    {
        public static CmdResult Run(LoadOptions opts)
        {
            var subscription = opts.GetSubscription();

            var client = new AzureClient(subscription);

            var resources = new Resources();

            client.FillInExistingResourcesAsync(resources: resources).ConfigureAwait(false).GetAwaiter().GetResult();
            client.UpdateBillingResourcesAsync(resources: resources).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine();
            Console.WriteLine($"|{"Resource",-40}|{"Group",-40}|{"Type",-40}|{"Cost",10}|");

            foreach (var resource in resources.OrderByDescending(item => item.Cost))
                Console.WriteLine($"|{resource.Name.Ellipsis(40),-40}|{resource.Group.Ellipsis(40),-40}|{resource.Type.Ellipsis(40),-40}|{resource.Cost,10:N6}|");

            return CmdResult.Default;
        }
    }
}
