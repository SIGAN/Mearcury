using CommandLine;

namespace Mearcury.Admin.Core
{
    public class CoreOptions
    {
        [Option(Default = "30b09a3f-da64-4d9a-859e-49f4b992f12b", HelpText = "Application ID of the service principal (i.e. a513a020-c109-47b4-b739-c56783a679e8).")]
        public string ClientId { get; set; }

        [Option(Required = false, Default = null, HelpText = "Application Secret.")]
        public string ClientSecret { get; set; }

        [Option(Default = "EPAM.onmicrosoft.com", HelpText = "Azure Active Directory tenant id (i.e. EPAM.onmicrosoft.com).")]
        public string TenantId { get; set; }

        [Option(Default = "fbef36e8-2c5c-4483-9fb8-5278903da739", HelpText = "Azure Subscription Id (i.e. fbef36e8-2c5c-4483-9fb8-5278903da739).")]
        public string SubscriptionId { get; set; }

        [Option(Default = "MS-AZR-0063P", HelpText = "Subscription Offer ID (i.e. MS-AZR-0063P).")]
        public string OfferId { get; set; }

        [Option(Default = "USD", HelpText = "Currency to use (i.e. USD).")]
        public string Currency { get; set; }

        [Option(Default = "en-US", HelpText = "Local code to use (i.e. en-US).")]
        public string Locale { get; set; }

        [Option(Default = "US", HelpText = "2 letter ISO country code where the offer was purchased (i.e. US).")]
        public string Region { get; set; }

        [Option(Default = "AzureGlobalCloud", HelpText = "Name of Azure Environment (i.e. AzureGlobalCloud)")]
        public string Environment { get; set; }
    }
}
