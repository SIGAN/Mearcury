using Mearcury.Entities;
using Mearcury.Providers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mearcury.Subscriptions
{
    public class Subscription : TenantEntityBase<Guid>
    {
        [ForeignKey("Provider")]
        public Guid ProviderId { get; set; }

        public Provider Provider { get; set; }

        public string SubscriptionId { get; set; }

        public string OfferId { get; set; }

        public string Currency { get; set; }

        public string Locale { get; set; }

        public string Region { get; set; }

        public string Environment { get; set; }
    }
}
