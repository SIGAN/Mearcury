using System.Collections;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public class IndexedCollectionWrapper<T> : IndexedWrapperBase<T, ICollection<T>>, ICollection<T>
    {
        public int Count => Source.Count;
        public bool IsReadOnly => Source.IsReadOnly;

        public IndexedCollectionWrapper(ICollection<T> source = null)
            : base(source ?? new List<T>())
        {
        }

        public void Add(T item)
        {
            Source.Add(item);
            UpdateIndexOnAdd(item);
        }

        public void Clear()
        {
            Source.Clear();
            UpdateIndexOnClear();
        }

        public bool Contains(T item)
        {
            return Source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Source.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var removed = Source.Remove(item);
            if (removed)
            {
                UpdateIndexOnRemove(item);
            }

            return removed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}
