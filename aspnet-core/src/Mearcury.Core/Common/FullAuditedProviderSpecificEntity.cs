using System.ComponentModel.DataAnnotations.Schema;
using Mearcury.Core.Providers;

namespace Mearcury.Core.Common
{
    public class FullAuditedProviderSpecificEntity<TPrimaryKey> : FullAuditedNamedEntity<TPrimaryKey>, IHasProvider
    {
        [ForeignKey(nameof(CloudProvider))]
        public int ProviderId { get; set; }
    }
}