using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mearcury.Core.Billing;
using Mearcury.Core.Common;

namespace Mearcury.Billing
{
    public class CloudProviderMetric : FullAuditedProviderSpecificEntity<int>
    {
        [Required]
        [StringLength(2048)]
        public string ProviderCode { get; set; }

        [ForeignKey(nameof(StandardMetric))]
        public int StandardMetricId { get; set; }
    }
}
