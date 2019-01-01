using System;

namespace Mearcury.Core
{
    public class Subscription
    {
        public Subscription(
            string clientId,
            string clientSecret,
            string tenantId,
            string subscriptionId,
            string offerId,
            string currency,
            string locale,
            string region,
            string environment)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentException($"{nameof(clientId)} is required!", nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                clientSecret = null;

            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException($"{nameof(tenantId)} is required!", nameof(tenantId));

            if (string.IsNullOrWhiteSpace(subscriptionId))
                throw new ArgumentException($"{nameof(subscriptionId)} is required!", nameof(subscriptionId));

            if (string.IsNullOrWhiteSpace(offerId))
                throw new ArgumentException($"{nameof(offerId)} is required!", nameof(offerId));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException($"{nameof(currency)} is required!", nameof(currency));

            if (string.IsNullOrWhiteSpace(locale))
                throw new ArgumentException($"{nameof(locale)} is required!", nameof(locale));

            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException($"{nameof(region)} is required!", nameof(region));

            if (string.IsNullOrWhiteSpace(environment))
                throw new ArgumentException($"{nameof(environment)} is required!", nameof(environment));

            ClientId = clientId;
            ClientSecret = clientSecret;
            SubscriptionId = subscriptionId;
            TenantId = tenantId;
            OfferId = offerId;
            Currency = currency;
            Locale = locale;
            Region = region;
            Environment = environment;

            Authority = $"https://login.microsoftonline.com/{TenantId}";
        }

        public string ClientId { get; }

        public string ClientSecret { get; }

        public string TenantId { get; }

        public string SubscriptionId { get; }

        public string OfferId { get; }

        public string Currency { get; }

        public string Locale { get; }

        public string Region { get; }

        public string Environment { get; }

        public string RedirectUri { get; } = "http://localhost/mearcury";

        public string Authority { get; }
    }
}
