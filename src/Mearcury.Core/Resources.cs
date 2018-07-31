using System;
using System.Collections;
using System.Collections.Generic;

namespace Mearcury.Core
{
    public class Resources: IEnumerable<Resource>
    {
        private readonly Dictionary<string, Resource> _store = new Dictionary<string, Resource>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<string>> _names = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<string>> _groups = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public Resources()
        {
        }

        public Resources(Resources existing)
        {
            if (existing == null)
                throw new ArgumentNullException(nameof(existing));

            foreach (var resource in existing)
                Add(resource);
        }

        public void Add(string id, string name, string group, string type)
        {
            Add(new Resource(id, name, group, type));
        }

        public void Add(Resource resource)
        {
            _store.Add(resource.Id, resource);

            if (!_names.TryGetValue(resource.Name, out var names))
                _names[resource.Name] = names = new List<string>();
            names.Add(resource.Id);

            if (!_groups.TryGetValue(resource.Group, out var groups))
                _groups[resource.Name] = groups = new List<string>();
            groups.Add(resource.Id);
        }

        public bool ExistsById(string id)
        {
            return _store.ContainsKey(id);
        }

        public Resource GetById(string id)
        {
            return _store[id];
        }

        public bool ExistsByName(string name)
        {
            return _names.ContainsKey(name);
        }

        public IEnumerable<Resource> GetByName(string name)
        {
            foreach (var id in _names[name])
                yield return _store[id];
        }

        public bool ExistsByGroup(string group)
        {
            return _groups.ContainsKey(group);
        }

        public IEnumerable<Resource> GetByGroup(string group)
        {
            foreach (var id in _groups[group])
                yield return _store[id];
        }

        public void Set(Resource resource)
        {
            Remove(resource);
            Add(resource);
        }

        public void Remove(Resource resource)
        {
            Remove(resource.Id);
        }

        public void Remove(string id)
        {
            if (_store.TryGetValue(id, out var resource))
            {
                var names = _names[resource.Name];
                names.Remove(id);
                if (names.Count == 0)
                    _names.Remove(resource.Name);

                var groups = _groups[resource.Group];
                groups.Remove(id);
                if (groups.Count == 0)
                    _groups.Remove(resource.Group);
            }

            _store.Remove(id);
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return _store.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _store.Values.GetEnumerator();
        }
    }
}
