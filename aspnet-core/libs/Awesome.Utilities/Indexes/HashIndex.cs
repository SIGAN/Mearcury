using System;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public class HashIndex<TEntry, TField> : DynamicIndexBase<TEntry, TField>
    {
        private readonly Dictionary<TField, List<TEntry>> _index;

        public HashIndex(Func<TEntry, TField> extractor, Func<TEntry, bool> filter = null)
            : base(extractor, filter)
        {
            _index = new Dictionary<TField, List<TEntry>>();
        }

        public HashIndex(IEqualityComparer<TField> comparer, Func<TEntry, TField> extractor, Func<TEntry, bool> filter = null)
            : base(extractor, filter)
        {
            _index = new Dictionary<TField, List<TEntry>>(comparer);
        }

        public override IEnumerable<TField> GetKeys()
        {
            return _index.Keys;
        }

        public override IEnumerable<TEntry> GetEntries()
        {
            foreach (var item in _index)
            {
                foreach (var entry in item.Value)
                {
                    yield return entry;
                }
            }
        }

        public override IDynamicIndex<TEntry> Clear()
        {
            _index.Clear();
            return this;
        }

        public override bool Exists(TField value)
        {
            return _index.ContainsKey(value);
        }

        public override IEnumerable<TEntry> Lookup(TField value)
        {
            if (_index.TryGetValue(value, out var entries))
                return entries;
            return Array.Empty<TEntry>();
        }

        protected override void Delete(IEnumerable<TEntry> entries)
        {
            foreach (var entry in entries)
            {
                var value = Extractor(entry);

                if (!_index.TryGetValue(value, out var entriesByValue))
                    continue;

                entriesByValue.Remove(entry);

                if (entriesByValue.Count == 0)
                    _index.Remove(value);
            }
        }

        protected override void Insert(IEnumerable<TEntry> entries)
        {
            foreach (var entry in entries)
            {
                var value = Extractor(entry);

                if (!_index.TryGetValue(value, out var entriesByValue))
                    _index[value] = entriesByValue = new List<TEntry>();

                entriesByValue.Add(entry);
            }
        }
    }
}
