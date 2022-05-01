using Abp.Domain.Entities.Auditing;

namespace Mearcury.Entities
{
    /// <summary>
    /// Defines entity with Name field
    /// </summary>
    public interface INamedEntity
    {
        /// <summary>
        /// Name of the entity
        /// </summary>
        string Name { get; set; }
    }
}
