using System;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public interface IRangeIndex<TEntry, TField>: IIndex<TEntry, TField>
    {
        IEnumerable<TEntry> LookupRange(TField from, TField to);
        IEnumerable<TEntry> LookupGt(TField value);
        IEnumerable<TEntry> LookupLt(TField value);
    }
}
