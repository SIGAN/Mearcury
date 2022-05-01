using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Mearcury.Billing;

namespace Mearcury.Core.Billing
{
    public class BillingSample : Entity<long>
    {
        /// <summary>
        /// Probably FK constraint should be removed
        /// </summary>
        /// <value></value>
        [ForeignKey(nameof(StandardMetric))]
        public int MetricId { get; set; }

        public double Quantity { get; set; }

        public double CostInUSD { get; set; }

        public double CostInUSD { get; set; }
        costInBillingCurrency
            costInPricingCurrency

exchangeRate

pricingCurrencyCode
            billingCurrencyCode
    exchangeRateDate
            exchangeRatePricingToBilling
    }
}
