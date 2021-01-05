using System.Collections;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public class IndexedDictionaryWrapper<TKey, TValue> : IndexedWrapperBase<KeyValuePair<TKey, TValue>, IDictionary<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        public IndexedDictionaryWrapper(IDictionary<TKey, TValue> source = null)
            : base(source ?? new Dictionary<TKey, TValue>())
        {
        }

        public TValue this[TKey key]
        {
            get => Source[key]; set
            {
                if (Source.TryGetValue(key, out var old))
                {
                    Source[key] = value;
                    UpdateIndexOnUpdate(new KeyValuePair<TKey, TValue>(key, old), new KeyValuePair<TKey, TValue>(key, value));
                }
                else
                {
                    Source[key] = value;
                    UpdateIndexOnAdd(new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        public ICollection<TKey> Keys => Source.Keys;

        public ICollection<TValue> Values => Source.Values;

        public int Count => Source.Count;

        public bool IsReadOnly => Source.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            Source.Add(key, value);
            UpdateIndexOnAdd(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Source.Add(item);
            UpdateIndexOnAdd(item);
        }

        public void Clear()
        {
            Source.Clear();
            UpdateIndexOnClear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Source.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return Source.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Source.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (!Source.TryGetValue(key, out var value))
            {
                return false;
            }

            var removed = Source.Remove(key);
            if (removed)
            {
                UpdateIndexOnRemove(new KeyValuePair<TKey, TValue>(key, value));
            }

            return removed;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var removed = Source.Remove(item);
            if (removed)
            {
                UpdateIndexOnRemove(item);
            }

            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Source.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}
