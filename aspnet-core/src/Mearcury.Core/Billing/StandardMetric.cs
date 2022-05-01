using System.ComponentModel.DataAnnotations;
using Mearcury.Billing;
using Mearcury.Core.Common;

namespace Mearcury.Core.Billing
{
    public class StandardMetric : FullAuditedProviderSpecificEntity<int>
    {
        [Required]
        [StringLength(1)]
        public MetricBase Base { get; set; }

        [Required]
        [StringLength(1)]
        public MetricCategory Category { get; set; }
    }
}