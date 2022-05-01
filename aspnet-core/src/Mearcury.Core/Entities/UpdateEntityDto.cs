namespace Mearcury.Entities
{
    public class UpdateEntityDto<TPrimaryKey> //: IUpdateEntityDto<TPrimaryKey>
    {
        public string Name { get; set; }
        public object OId => Id;

        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TPrimaryKey Id { get; set; }
    }
}
