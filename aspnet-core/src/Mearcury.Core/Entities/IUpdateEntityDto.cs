using Abp.Application.Services.Dto;

namespace Mearcury.Entities
{
    public interface IUpdateEntityDto<TPrimaryKey> : INamedEntity, IEntityWithOId, IEntityDto<TPrimaryKey>
    {
    }
}
