using Abp.Domain.Entities.Auditing;

namespace Mearcury.Entities
{
    /// <summary>
    /// Defines entity with OId field, which returns Id as an object
    /// </summary>
    public interface IEntityWithOId
    {
        /// <summary>
        /// Returns entity id as an object
        /// </summary>
        object OId { get; }
    }
}
