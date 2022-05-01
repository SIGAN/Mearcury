using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Mearcury.Core.Common
{
    public class FullAuditedNamedEntity<TPrimaryKey> : FullAuditedEntity<TPrimaryKey>, IHasName
    {
        [Required]
        [StringLength(MearcuryConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}