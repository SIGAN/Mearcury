using CodeHollow.AzureBillingApi;
using Mearcury.Core;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using System;
using System.Threading.Tasks;

namespace Mearcury.Azure
{
    public class AzureClient
    {
        private readonly Lazy<AzureIdentity> _identity;
        private readonly Lazy<Client> _billing;
        private readonly Lazy<IAzure> _management;

        public AzureClient(Subscription subscription)
        {
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
            Environment = AzureEnvironment.FromName(Subscription.Environment);

            _identity = new Lazy<AzureIdentity>(GetIdentity, true);
            _billing = new Lazy<Client>(GetBilling, true);
            _management = new Lazy<IAzure>(GetAzure, true);
        }

        /// <summary>
        /// Azure Billing API
        /// </summary>
        public Client Billing => _billing.Value;

        /// <summary>
        /// Azure Management API
        /// </summary>
        public IAzure Management => _management.Value;

        /// <summary>
        /// Azure Authentication Identity
        /// </summary>
        public AzureIdentity Identity => _identity.Value;

        /// <summary>
        /// Azure Subscription details
        /// </summary>
        public Subscription Subscription { get; }

        /// <summary>
        /// Azure Environment details
        /// </summary>
        public AzureEnvironment Environment { get; }

        public Task<string> GetTokenAsync(string resource, string scope = null)
        {
            return Identity.GetTokenAsync(Subscription.Authority, resource, scope);
        }

        public Task<string> GetManagementTokenAsync()
        {
            return GetTokenAsync(Environment.ManagementEndpoint);
        }

        public Task<string> GetGraphTokenAsync()
        {
            return GetTokenAsync(Environment.GraphEndpoint);
        }

        private AzureIdentity GetIdentity()
        {
            return
                string.IsNullOrWhiteSpace(Subscription.ClientSecret)
                ? new AzureIdentity(Subscription.RedirectUri, Subscription.ClientId)
                : new AzureIdentity(Subscription.RedirectUri, Subscription.ClientId, Subscription.ClientSecret);
        }

        private Client GetBilling()
        {
            return new Client(Subscription.TenantId, Subscription.ClientId, Subscription.ClientSecret, Subscription.SubscriptionId, Subscription.RedirectUri);
        }

        private IAzure GetAzure()
        {
            var creds =
                new AzureCredentials(
                    new AzureAuthenticationServiceCredentials(Identity, Subscription.Authority, Environment.ResourceManagerEndpoint),
                    new AzureAuthenticationServiceCredentials(Identity, Subscription.Authority, Environment.GraphEndpoint),
                    Subscription.TenantId,
                    Environment)
                .WithDefaultSubscription(Subscription.SubscriptionId);

            return Microsoft.Azure.Management.Fluent.Azure.Authenticate(creds).WithSubscription(Subscription.SubscriptionId);
        }
    }
}
