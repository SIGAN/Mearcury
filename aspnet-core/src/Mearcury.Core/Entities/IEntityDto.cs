using Abp.Domain.Entities.Auditing;

namespace Mearcury.Entities
{
    public interface IEntityDto<TPrimaryKey> : IFullAudited, INamedEntity, IEntityWithOId, Abp.Application.Services.Dto.IEntityDto<TPrimaryKey>
    {
    }
}
