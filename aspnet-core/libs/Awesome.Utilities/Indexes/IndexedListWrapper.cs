using System.Collections;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public class IndexedListWrapper<T> : IndexedWrapperBase<T, IList<T>>, IList<T>
    {
        public IndexedListWrapper(IList<T> source = null)
            : base(source ?? new List<T>())
        {
        }

        public T this[int index]
        {
            get => Source[index];
            set
            {
                var old = Source[index];
                Source[index] = value;
                UpdateIndexOnUpdate(old, value);
            }
        }

        public int Count => Source.Count;

        public bool IsReadOnly => Source.IsReadOnly;

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

        public IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return Source.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Source.Insert(index, item);
            UpdateIndexOnAdd(item);
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

        public void RemoveAt(int index)
        {
            var item = Source[index];
            Source.RemoveAt(index);
            UpdateIndexOnRemove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}
