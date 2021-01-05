using System;
using System.Collections.Generic;

namespace Awesome.Indexes
{
    public interface IIndex<TEntry, TField>
    {
        IEnumerable<TField> GetKeys();
        IEnumerable<TEntry> GetEntries();

        bool Exists(TField value);
        IEnumerable<TEntry> Lookup(TField value);
    }
}
