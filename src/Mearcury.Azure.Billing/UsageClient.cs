using Mearcury.Azure.Billing.Usage;
using Mearcury.Azure.Core;
using Mearcury.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mearcury.Azure.Billing
{
    /// <summary>
    /// Reads data from the Azure billing usage REST API.
    /// Uses version 2015-06-01-preview (https://msdn.microsoft.com/en-us/library/azure/mt219003.aspx)
    /// </summary>
    public class UsageClient : Client
    {
        private static readonly string APIVERSION = "2015-06-01-preview";

        /// <summary>
        /// Creates the client to read data from the usage REST API.
        /// </summary>
        /// <param name="azureIdentity">the service credentials</param>
        /// <param name="environment">the azure environment</param>
        /// <param name="subscription">the subscription details</param>
        public UsageClient(AzureIdentity azureIdentity, AzureEnvironment environment, Subscription subscription)
            : base(azureIdentity, environment, subscription)
        {
        }

        /// <summary>
        /// Reads data from the usage REST API.
        /// </summary>
        /// <param name="startDate">Start date - get usage date from this date</param>
        /// <param name="endDate">End date - get usage data to this date</param>
        /// <param name="granularity">The granularity - daily or hourly</param>
        /// <param name="showDetails">Include instance-level details or not</param>
        /// <returns>the usage data for the given time range with the given granularity</returns>
        public async Task<UsageData> GetAsync(DateTime startDate, DateTime endDate, AggregationGranularity granularity, bool showDetails)
        {
            if (startDate >= endDate)
                throw new ArgumentException("Start date must be before the end date!");

            if (endDate >= DateTime.Now.AddHours(-1))
                endDate = DateTime.Now.AddHours(-1).ToUniversalTime();

            var startTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, DateTimeKind.Utc);
            var endTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0, DateTimeKind.Utc);

            if (granularity == AggregationGranularity.Hourly)
            {
                startTime = startTime.AddHours(startDate.Hour);
                endTime = endTime.AddHours(endDate.Hour);
            }

            var st = WebUtility.UrlEncode(startTime.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            var et = WebUtility.UrlEncode(endTime.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            var url =
                $"https://management.azure.com/subscriptions/{Subscription.SubscriptionId}" +
                $"/providers/Microsoft.Commerce/UsageAggregates" +
                $"?api-version={APIVERSION}" +
                $"&reportedStartTime={st}&reportedEndTime={et}" +
                $"&aggregationGranularity={granularity.ToString()}" +
                $"&showDetails={showDetails.ToString().ToLower()}";

            var data = await GetDataAsync(url);

            if (String.IsNullOrEmpty(data))
                return null;

            var usageData = JsonConvert.DeserializeObject<UsageData>(data);

            // read data from the usagedata api as long as the continuationtoken is set.
            // usage data api returns 1000 values per api call, to receive all values,  
            // we have to call the url stored in nextLink property.
            while (!String.IsNullOrEmpty(usageData.NextLink))
            {
                var next = await GetDataAsync(usageData.NextLink);
                var nextUsageData = JsonConvert.DeserializeObject<UsageData>(next);

                usageData.Values.AddRange(nextUsageData.Values);
                usageData.NextLink = nextUsageData.NextLink;
            }

            return usageData;
        }
    }
}
