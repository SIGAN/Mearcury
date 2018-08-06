using Mearcury.Azure.Billing.RateCard;
using Mearcury.Azure.Core;
using Mearcury.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Mearcury.Azure.Billing
{
    /// <summary>
    /// Reads data from the Azure billing ratecard REST API. 
    /// Uses version 2016-08-31-preview (https://msdn.microsoft.com/en-us/library/azure/mt219004.aspx)
    /// </summary>
    public class RateCardClient : Client
    {
        private static readonly string APIVERSION = "2016-08-31-preview"; // "2015-06-01-preview";

        /// <summary>
        /// Creates the client to read data from the ratecard REST API. Uses user authentication for 
        /// authentication. Opens a popup to enter user and password
        /// </summary>
        /// <param name="azureIdentity">the service credentials</param>
        /// <param name="environment">the azure environment</param>
        /// <param name="subscription">the subscription details</param>
        public RateCardClient(AzureIdentity azureIdentity, AzureEnvironment environment, Subscription subscription)
            : base(azureIdentity, environment, subscription)
        {
        }

        /// <summary>
        /// Reads the ratecard data from the REST API with an existing OAuth token. Useful if you want to minimize 
        /// the authorizations
        /// </summary>
        /// <param name="offerDurableId">Offer - e.g. MS-AZR-0003p (see: https://azure.microsoft.com/en-us/support/legal/offer-details/) </param>
        /// <param name="currency">the currency - e.g. EUR or USD</param>
        /// <param name="locale">the locale - e.g. de-AT or en-US</param>
        /// <param name="regionInfo">the region - e.g. DE, AT or US</param>
        /// <returns>The ratecard information</returns>
        public async Task<RateCardData> GetAsync()
        {
            var url =
                $"https://management.azure.com/subscriptions/{Subscription}/providers/Microsoft.Commerce/RateCard" +
                $"?api-version={APIVERSION}" +
                $"&$filter=OfferDurableId eq '{Subscription.OfferId}' and Currency eq '{Subscription.Currency}' and Locale eq '{Subscription.Locale}' and RegionInfo eq '{Subscription.Region}'";

            var data = await GetDataAsync(url);

            return JsonConvert.DeserializeObject<RateCardData>(data);
        }
    }
}
