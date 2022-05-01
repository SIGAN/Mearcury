using System;
using System.Collections.Generic;
using System.Text;

namespace Mearcury.Admin.Core
{
    public class CmdResult
    {
        internal static readonly CmdResult Default = new CmdResult();
        internal static readonly CmdResult NoWait = new CmdResult() { Wait = false };

        public bool Wait { get; set; } = true;
    }
}
