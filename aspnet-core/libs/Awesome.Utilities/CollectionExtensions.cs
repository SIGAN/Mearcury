using Awesome.Indexes;
using System;
using System.Collections.Generic;

namespace Awesome
{
    public static class CollectionExtensions
    {
        public static HashIndex<TEntry, TField> SetAsHashOf<TEntry, TField, TTarget>(
            this IndexedWrapperBase<TEntry, TTarget>.IndexAccessor<TField> accessor,
            Func<TEntry, TField> extractor,
            Func<TEntry, bool> filter = null,
            IEqualityComparer<TField> comparer = null)
            where TTarget : IEnumerable<TEntry>
        {
            return accessor.SetAs(
                target => {
                    var index = comparer == null
                        ? new HashIndex<TEntry, TField>(extractor, filter)
                        : new HashIndex<TEntry, TField>(comparer, extractor, filter);
                    index.Build(target);
                    return index;
                });
        }

        public static HashIndex<TEntry, TField> HashIndexOf<TEntry, TField, TTarget>(
            this IndexedWrapperBase<TEntry, TTarget> wrapper,
            string name,
            Func<TEntry, TField> extractor,
            Func<TEntry, bool> filter = null,
            IEqualityComparer<TField> comparer = null)
            where TTarget : IEnumerable<TEntry>
        {
            return wrapper.Index<TField>(name).SetAsHashOf(extractor, filter, comparer);
        }

        public static IndexedCollectionWrapper<T> WrapForIndexing<T>(this ICollection<T> target)
        {
            return new IndexedCollectionWrapper<T>(target);
        }

        public static IndexedListWrapper<T> WrapForIndexing<T>(this IList<T> target)
        {
            return new IndexedListWrapper<T>(target);
        }

        public static IndexedDictionaryWrapper<TKey, TValue> WrapForIndexing<TKey, TValue>(this IDictionary<TKey, TValue> target)
        {
            return new IndexedDictionaryWrapper<TKey, TValue>(target);
        }
    }
}
