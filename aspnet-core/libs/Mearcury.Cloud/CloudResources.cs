using Awesome;
using Awesome.Indexes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mearcury.Cloud
{

#warning Make Index storage to be able to be Indexed as well (nested, i.e. resourcesByRegion.Lookup().ResourcesByGroup().Lookup())! By separating connectors, providers and subscribers.

    public class CloudResources : IEnumerable<CloudResource>
    {
        protected readonly IndexedListWrapper<CloudResource> resources;

        protected readonly IIndex<CloudResource, string> resourcesById;
        protected readonly IIndex<CloudResource, string> resourcesByName;
        protected readonly IIndex<CloudResource, string> resourcesByGroup;
        protected readonly IIndex<CloudResource, string> resourcesByRegion;

        public CloudResources()
        {
            resources = new IndexedListWrapper<CloudResource>();

            resourcesById = resources.HashIndexOf("id", resource => resource.Id, comparer: StringComparer.OrdinalIgnoreCase);
            resourcesByName = resources.HashIndexOf("name", resource => resource.Name, comparer: StringComparer.OrdinalIgnoreCase);
            resourcesByName = resources.HashIndexOf("group", resource => resource.GroupName, comparer: StringComparer.OrdinalIgnoreCase);
            resourcesByName = resources.HashIndexOf("region", resource => resource.RegionName, comparer: StringComparer.OrdinalIgnoreCase);
        }

        public void Add(CloudResource resource)
        {
            resources.Add(resource);
        }

        public bool ExistsById(string id)
        {
            return resourcesById.Exists(id);
        }

        public CloudResource GetById(string id)
        {
            return resourcesById.Lookup(id).FirstOrDefault();
        }

        public bool ExistsByName(string name)
        {
            return resourcesByName.Exists(name);
        }

        public IEnumerable<CloudResource> GetByName(string name)
        {
            return resourcesByName.Lookup(name);
        }

        public bool ExistsByGroup(string group)
        {
            return resourcesByGroup.Exists(group);
        }

        public IEnumerable<CloudResource> GetByGroup(string group)
        {
            return resourcesByGroup.Lookup(group);
        }

        public bool UpdateCostById(string id, double cost)
        {
            if (!ExistsById(id))
            {
                return false;
            }

            GetById(id).Cost += cost;

            return true;
        }

        public bool UpdateCostByName(string name, double cost)
        {
            if (!ExistsByName(name))
            {
                return false;
            }

            GetByName(name).First().Cost += cost;

            return true;
        }

        public void Set(CloudResource resource)
        {
            Remove(resource);
            Add(resource);
        }

        public void Remove(CloudResource resource)
        {
            resources.Remove(resource);
        }

        public void Remove(string id)
        {
            if (!ExistsById(id))
            {
                return;
            }

            Remove(GetById(id));
        }

        public IEnumerator<CloudResource> GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return resources.GetEnumerator();
        }
    }
}
