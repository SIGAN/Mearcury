using System;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public interface IDynamicIndex<TEntry>
    {
        IDynamicIndex<TEntry> Build(IEnumerable<TEntry> entries);
        IDynamicIndex<TEntry> Clear();

        IDynamicIndex<TEntry> Add(params TEntry[] entries);
        IDynamicIndex<TEntry> Add(IEnumerable<TEntry> entries);
        IDynamicIndex<TEntry> Add<T>(IEnumerable<T> entries, Func<T, TEntry> converter);

        IDynamicIndex<TEntry> Remove(params TEntry[] entries);
        IDynamicIndex<TEntry> Remove(IEnumerable<TEntry> entries);
        IDynamicIndex<TEntry> Remove<T>(IEnumerable<T> entries, Func<T, TEntry> converter);
    }
}
