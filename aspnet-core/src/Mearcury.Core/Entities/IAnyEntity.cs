using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Mearcury.Entities
{
    public interface IAnyEntity<TPrimaryKey> : IFullAudited, INamedEntity, IEntityWithOId, IEntity<TPrimaryKey>
    {
    }
}
