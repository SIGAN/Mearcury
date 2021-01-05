using System;
using System.Collections.Generic;

namespace Mearcury.Cloud
{
    public class CloudResource
    {
        [Obsolete]
        public static CloudResource DeletedResourceFromName(string resourceId, string resourceName, IReadOnlyDictionary<string, string> resourceTags)
        {
            return new CloudResource { Id = resourceId, Name = resourceName, Tags = resourceTags, GroupName = "deleted", RegionName = "deleted", Type = "deleted", Cost = 0, Load = 0 };
        }

        public string Id;
        public string Name;
        public string Type;
        public string GroupName;
        public string RegionName;

        public IReadOnlyDictionary<string, string> Tags;

        public double Load;
        public double Cost;
    }
}
