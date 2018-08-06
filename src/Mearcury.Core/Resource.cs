using System;
using System.Collections.Generic;

namespace Mearcury.Core
{
    public class Resource
    {
        public static Resource DeletedResourceFromName(string id, string name, IReadOnlyDictionary<string, string> tags)
        {
            return new Resource(id, name, "deleted", "deleted", tags);
        }

        public string Id { get; }
        public string Name { get; }
        public string Group { get; }
        public string Type { get; }

        public IReadOnlyDictionary<string, string> Tags { get; }

        public double Load { get; set; }
        public double Cost { get; set; }

        public Resource(string id, string name, string group, string type, IReadOnlyDictionary<string, string> tags)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Group = group ?? throw new ArgumentNullException(nameof(group));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));

            Load = 0;
            Cost = 0;
        }
    }
}
