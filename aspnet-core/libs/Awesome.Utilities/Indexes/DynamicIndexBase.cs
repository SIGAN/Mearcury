using System;
using System.Collections.Generic;
using System.Linq;

namespace Awesome.Indexes
{
    public abstract class DynamicIndexBase<TEntry, TField> : IDynamicIndex<TEntry>, IIndex<TEntry, TField>
    {
        public Func<TEntry, TField> Extractor { get; }
        public Func<TEntry, bool> Filter { get; }

        public DynamicIndexBase(Func<TEntry, TField> extractor, Func<TEntry, bool> filter = null)
        {
            Extractor = extractor;
            Filter = filter;
        }

        protected virtual IEnumerable<TEntry> Preprocess(IEnumerable<TEntry> entries)
        {
            return Filter == null ? entries : entries.Where(Filter);
        }

        protected virtual IEnumerable<TEntry> Preprocess<T>(IEnumerable<T> entries, Func<T, TEntry> converter)
        {
            return Filter == null ? entries.Select(converter) : entries.Select(converter).Where(Filter);
        }

        public virtual IDynamicIndex<TEntry> Build(IEnumerable<TEntry> entries)
        {
            Clear();
            Insert(Preprocess(entries));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Add(params TEntry[] entries)
        {
            Insert(Preprocess(entries));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Add(IEnumerable<TEntry> entries)
        {
            Insert(Preprocess(entries));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Add<T>(IEnumerable<T> entries, Func<T, TEntry> converter)
        {
            Insert(Preprocess(entries, converter));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Remove(params TEntry[] entries)
        {
            Delete(Preprocess(entries));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Remove(IEnumerable<TEntry> entries)
        {
            Delete(Preprocess(entries));
            return this;
        }

        public virtual IDynamicIndex<TEntry> Remove<T>(IEnumerable<T> entries, Func<T, TEntry> converter)
        {
            Delete(Preprocess(entries, converter));
            return this;
        }

        protected abstract void Insert(IEnumerable<TEntry> entries);
        protected abstract void Delete(IEnumerable<TEntry> entries);

        public abstract IEnumerable<TField> GetKeys();
        public abstract IEnumerable<TEntry> GetEntries();

        public abstract bool Exists(TField value);
        public abstract IEnumerable<TEntry> Lookup(TField value);

        public abstract IDynamicIndex<TEntry> Clear();
    }
}
