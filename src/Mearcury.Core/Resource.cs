using System;

namespace Mearcury.Core
{
    public class Resource
    {
        public static Resource DeletedResourceFromName(string id, string name)
        {
            return new Resource(id, name, "deleted", "deleted");
        }

        public string Id { get; }
        public string Name { get; }
        public string Group { get; }
        public string Type { get; }

        public double Load { get; set; }
        public double Cost { get; set; }

        public Resource(string id, string name, string group, string type)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Group = group ?? throw new ArgumentNullException(nameof(group));
            Type = type ?? throw new ArgumentNullException(nameof(type));

            Load = 0;
            Cost = 0;
        }
    }
}
