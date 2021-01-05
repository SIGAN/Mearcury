using System;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public class IndexedWrapperBase<TEntry, TTarget>
        where TTarget : IEnumerable<TEntry>
    {
        public TTarget Source { get; }

        protected readonly Dictionary<string, IDynamicIndex<TEntry>> Indexes = new Dictionary<string, IDynamicIndex<TEntry>>(StringComparer.OrdinalIgnoreCase);

        public IndexedWrapperBase(TTarget source)
        {
            Source = source;
        }

        protected void UpdateIndexOnAdd(TEntry item)
        {
            foreach (var index in Indexes.Values)
            {
                index.Add(item);
            }
        }

        protected void UpdateIndexOnUpdate(TEntry oldItem, TEntry newItem)
        {
            UpdateIndexOnRemove(oldItem);
            UpdateIndexOnAdd(newItem);
        }

        protected void UpdateIndexOnRemove(TEntry item)
        {
            foreach (var index in Indexes.Values)
            {
                index.Remove(item);
            }
        }

        protected void UpdateIndexOnClear()
        {
            foreach (var index in Indexes.Values)
            {
                index.Clear();
            }
        }

        public virtual IndexAccessor<TField> Index<TField>(string name)
        {
            return new IndexAccessor<TField>(this, name);
        }

        public virtual RangeIndexAccessor<TField> RangeIndex<TField>(string name)
        {
            return new RangeIndexAccessor<TField>(this, name);
        }

        public class IndexAccessorBase
        {
            protected readonly IndexedWrapperBase<TEntry, TTarget> _wrapper;
            protected readonly string _name;

            public IndexAccessorBase(IndexedWrapperBase<TEntry, TTarget> wrapper, string name)
            {
                _name = name;
                _wrapper = wrapper;
            }

            public virtual T SetAs<T>(T index)
                where T : class, IDynamicIndex<TEntry>
            {
                _wrapper.Indexes[_name] = index;
                return index;
            }

            public virtual T SetAs<T>(Func<TTarget, T> creator)
                where T : class, IDynamicIndex<TEntry>
            {
                T index;

                if (!_wrapper.Indexes.ContainsKey(_name))
                {
                    _wrapper.Indexes[_name] = index = creator(_wrapper.Source);
                }
                else
                {
                    index = _wrapper.Indexes[_name] as T;

                    if (index == null)
                    {
                        throw new NotSupportedException($"Index '{_name}' does not support '{typeof(T).FullName}' interface!");
                    }
                }

                return index;
            }

            public virtual IndexedWrapperBase<TEntry, TTarget> Remove()
            {
                _wrapper.Indexes.Remove(_name);
                return _wrapper;
            }
        }

        public class IndexAccessor<TField> : IndexAccessorBase, IIndex<TEntry, TField>
        {
            private readonly IIndex<TEntry, TField> _index;

            public IndexAccessor(IndexedWrapperBase<TEntry, TTarget> wrapper, string name)
                : base(wrapper, name)
            {
                _index = _wrapper.Indexes[_name] as IIndex<TEntry, TField>;

                if (_index == null)
                {
                    throw new NotSupportedException($"Index '{_name}' does not support '{nameof(IIndex<TEntry, TField>)}' interface!");
                }
            }

            public IEnumerable<TField> GetKeys()
            {
                return _index.GetKeys();
            }

            public IEnumerable<TEntry> GetEntries()
            {
                return _index.GetEntries();
            }

            public IEnumerable<TEntry> Lookup(TField value)
            {
                return _index.Lookup(value);
            }

            public bool Exists(TField value)
            {
                return _index.Exists(value);
            }
        }

        public class RangeIndexAccessor<TField> : IndexAccessorBase, IRangeIndex<TEntry, TField>
        {
            private readonly IRangeIndex<TEntry, TField> _index;

            public RangeIndexAccessor(IndexedWrapperBase<TEntry, TTarget> wrapper, string name)
                : base(wrapper, name)
            {
                _index = _wrapper.Indexes[_name] as IRangeIndex<TEntry, TField>;

                if (_index == null)
                {
                    throw new NotSupportedException($"Index '{_name}' does not support '{nameof(IRangeIndex<TEntry, TField>)}' interface!");
                }
            }

            public IEnumerable<TField> GetKeys()
            {
                return _index.GetKeys();
            }

            public IEnumerable<TEntry> GetEntries()
            {
                return _index.GetEntries();
            }

            public IEnumerable<TEntry> Lookup(TField value)
            {
                return _index.Lookup(value);
            }

            public IEnumerable<TEntry> LookupGt(TField value)
            {
                return _index.LookupGt(value);
            }

            public IEnumerable<TEntry> LookupLt(TField value)
            {
                return _index.LookupLt(value);
            }

            public IEnumerable<TEntry> LookupRange(TField from, TField to)
            {
                return _index.LookupRange(from, to);
            }

            public bool Exists(TField value)
            {
                return _index.Exists(value);
            }
        }
    }
}
