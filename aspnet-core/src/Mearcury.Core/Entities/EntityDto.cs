namespace Mearcury.Entities
{
    public class EntityDto<TPrimaryKey> : Abp.Application.Services.Dto.FullAuditedEntityDto<TPrimaryKey>, IEntityDto<TPrimaryKey>
    {
        object IEntityWithOId.OId => Id;
        public string Name { get; set; }
    }
}
