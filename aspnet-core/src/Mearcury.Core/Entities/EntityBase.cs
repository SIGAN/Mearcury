using Abp.Domain.Entities.Auditing;

namespace Mearcury.Entities
{
    /// <summary>
    /// Base class for entities in Mearcury project.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the PrimaryKey</typeparam>
    public class EntityBase<TPrimaryKey> : FullAuditedEntity<TPrimaryKey>, IAnyEntity<TPrimaryKey>
    {
        public virtual object OId => Id;
        public virtual string Name { get; set; }
    }
}
