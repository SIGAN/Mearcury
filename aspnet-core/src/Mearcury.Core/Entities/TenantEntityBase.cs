using Abp.Domain.Entities;

namespace Mearcury.Entities
{
    public class TenantEntityBase<TPrimaryKey> : EntityBase<TPrimaryKey>, IMustHaveTenant
    {
        public int TenantId { get; set; }
    }
}
