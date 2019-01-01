using Mearcury.Azure.Billing.RateCard;
using Mearcury.Azure.Billing.Usage;
using Mearcury.Azure.Core;
using Mearcury.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mearcury.Azure.Billing
{
    /// <summary>
    /// Client to read data from the Azure billing REST APIs.
    /// Allows to get data from the usage api <see cref="UsageClient"/>, from
    /// the ratecard api <see cref="RateCardClient"/> or get the combination of
    /// the usage and ratecard data <see cref="GetResourceCosts(string, string, string, string, DateTime, DateTime, AggregationGranularity, bool, string)"/>.
    /// Requires registration of an application in the active directory configuration in the azure portal.
    /// Please check https://github.com/codehollow/AzureBillingApi for more details.
    /// </summary>
    /// <example>
    /// Client c = new Client("[TENANT].onmicrosoft.com", "[CLIENTID]", "[CLIENTSECRET]", "[SUBSCRIPTIONID]", "http://[REDIRECTURL]");
    /// var resourceData = c.GetResourceCosts("MS-AZR-0003p", "EUR", "de-AT", "AT", new DateTime(2017, 1, 14, 0, 0, 0), new DateTime(2017, 2, 26, 23, 0, 0), AggregationGranularity.Daily, true);
    /// Console.WriteLine(resourceData.TotalCosts + " " + resourceData.RateCardData.Currency);
    /// </example>
    public class Client
    {
        // correct usage of HttpClient as described here: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// the service credentials
        /// </summary>
        public AzureIdentity AzureIdentity { get; }

        /// <summary>
        /// the azure environment
        /// </summary>
        public AzureEnvironment Environment { get; }

        /// <summary>
        /// The subscription
        /// </summary>
        public Subscription Subscription { get; }

        /// <summary>
        /// Creates the client to read data from the billing REST APIs.
        /// </summary>
        /// <param name="azureIdentity">the service credentials</param>
        /// <param name="environment">the azure environment</param>
        /// <param name="subscription">the subscription details</param>
        public Client(AzureIdentity azureIdentity, AzureEnvironment environment, Subscription subscription)
        {
            AzureIdentity = azureIdentity ?? throw new ArgumentNullException(nameof(azureIdentity));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
        }

        /// <summary>
        /// Reads data from the usage REST API.
        /// </summary>
        /// <param name="startDate">Start date - get usage date from this date</param>
        /// <param name="endDate">End date - get usage data to this date</param>
        /// <param name="granularity">The granularity - daily or hourly</param>
        /// <param name="showDetails">Include instance-level details or not</param>
        /// <param name="token">the OAuth token</param>
        /// <returns>the usage data for the given time range with the given granularity</returns>
        public Task<UsageData> GetUsageDataAsync(DateTime startDate, DateTime endDate, AggregationGranularity granularity = AggregationGranularity.Hourly, bool showDetails = true)
        {
            var uclient = new UsageClient(AzureIdentity, Environment, Subscription);
            return uclient.GetAsync(startDate, endDate, granularity, showDetails);
        }

        /// <summary>
        /// Reads the ratecard data from the REST API with an existing OAuth token. Useful if you want to minimize 
        /// the authorizations
        /// </summary>
        /// <param name="offerDurableId">Offer - e.g. MS-AZR-0003p (see: https://azure.microsoft.com/en-us/support/legal/offer-details/) </param>
        /// <param name="currency">the currency - e.g. EUR or USD</param>
        /// <param name="locale">the locale - e.g. de-AT or en-US</param>
        /// <param name="regionInfo">the region - e.g. DE, AT or US</param>
        /// <param name="token">the OAuth token</param>
        /// <returns>The ratecard information</returns>
        public Task<RateCardData> GetRateCardDataAsync()
        {
            var rcclient = new RateCardClient(AzureIdentity, Environment, Subscription);
            return rcclient.GetAsync();
        }

        /// <summary>
        /// Combines the ratecard data with the usage data.
        /// </summary>
        /// <param name="rateCardData">RateCard data</param>
        /// <param name="usageData">Usage data</param>
        /// <returns>The costs of the resources (combined data of ratecard and usage api)</returns>
        public static ResourceCostData Combine(RateCardData rateCardData, UsageData usageData)
        {
            ResourceCostData rcd = new ResourceCostData()
            {
                Costs = new List<ResourceCosts>(),
                RateCardData = rateCardData
            };

            // get all used meter ids
            var meterIds = (from x in usageData.Values select x.Properties.MeterId).Distinct().ToList();

            // aggregates all quantity and will be used to calculate costs (e.g. if quantity is included for free)
            // Dictionary<MeterId, Dictionary<YearAndMonthOfBillingCycle, aggregatedQuantity>>
            Dictionary<string, Dictionary<string, double>> aggQuant = meterIds.ToDictionary(x => x, x => new Dictionary<string, double>());

            foreach (var usageValue in usageData.Values)
            {
                string meterId = usageValue.Properties.MeterId;
                var rateCard = (from x in rateCardData.Meters where x.MeterId.Equals(meterId, StringComparison.OrdinalIgnoreCase) select x).FirstOrDefault();

                if (rateCard == null) // e.g. ApplicationInsights: there is no ratecard data for these
                    continue;

                var billingCycleId = GetBillingCycleIdentifier(usageValue.Properties.UsageStartTimeAsDate);
                if (!aggQuant[meterId].ContainsKey(billingCycleId))
                    aggQuant[meterId].Add(billingCycleId, 0.0);

                var usedQuantity = aggQuant[meterId][billingCycleId];

                var curcosts = GetMeterRate(rateCard.MeterRates, rateCard.IncludedQuantity, usedQuantity, usageValue.Properties.Quantity);

                aggQuant[meterId][billingCycleId] += usageValue.Properties.Quantity;

                rcd.Costs.Add(new ResourceCosts()
                {
                    RateCardMeter = rateCard,
                    UsageValue = usageValue,
                    CalculatedCosts = curcosts.Item1,
                    BillableUnits = curcosts.Item2
                });
            }

            return rcd;
        }

        /// <summary>
        /// Returns the costs for the given quantityToAdd for one billing cycle.
        /// </summary>
        /// <param name="meterRates">List of the meter rates</param>
        /// <param name="includedQuantity">Amount of included quantity (which is for free)</param>
        /// <param name="totalUsedQuantity">The already use quantity in the period</param>
        /// <param name="quantityToAdd">the quantity for which the calculation should be done</param>
        /// <returns>Tuple - Item1 = costs, Item2 = billableQuantity</returns>
        private static Tuple<double, double> GetMeterRate(Dictionary<double, double> meterRates, double includedQuantity, double totalUsedQuantity, double quantityToAdd)
        {
            Dictionary<double, double> modifiedMeterRates;

            // add included quantity to meter rates with cost 0
            if (includedQuantity > 0)
            {
                modifiedMeterRates = new Dictionary<double, double> { { 0, 0 } };

                foreach (var rate in meterRates)
                {
                    modifiedMeterRates.Add(rate.Key + includedQuantity, rate.Value);
                }
            }
            else
            {
                modifiedMeterRates = meterRates;
            }

            double costs = 0.0;
            double billableQuantity = 0.0;

            for (int i = modifiedMeterRates.Count; i > 0; i--)
            {
                var totalNew = totalUsedQuantity + quantityToAdd;
                var rate = modifiedMeterRates.ElementAt(i - 1);

                var tmp = totalNew - rate.Key;

                if (tmp >= 0)
                {
                    if (tmp > quantityToAdd)
                        tmp = quantityToAdd;

                    costs += tmp * rate.Value;

                    if (rate.Value > 0)
                        billableQuantity += tmp;

                    quantityToAdd -= tmp;
                    if (quantityToAdd == 0)
                        break;
                }
            }
            return new Tuple<double, double>(costs, billableQuantity);
        }

        /// <summary>
        /// Gets tokaen for the query
        /// </summary>
        /// <returns>JWT token</returns>
        protected Task<string> GetTokenAsync()
        {
            return AzureIdentity.GetTokenAsync(Subscription.Authority, Environment.ResourceManagerEndpoint);
        }

        /// <summary>
        /// Reads data from a url including the oauth token.
        /// </summary>
        /// <param name="url">service url</param>
        /// <returns>Result content</returns>
        protected async Task<string> GetDataAsync(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg =
                        "An error occurred! The service returned: " + response;

                    errorMsg +=
                        "Content: " + await response.Content.ReadAsStringAsync();

                    throw new Exception(errorMsg);
                }

                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Gets billing cycle identifier
        /// </summary>
        /// <param name="date">Requested data</param>
        /// <returns>Billing cycle identifier</returns>
        private static string GetBillingCycleIdentifier(DateTime date)
        {
            if (date.Day >= 14)
                return date.Year.ToString() + date.Month.ToString();

            var tmp = date.AddMonths(-1);
            return tmp.Year.ToString() + tmp.Month.ToString();
        }
    }
}
