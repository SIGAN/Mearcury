using Mearcury.Admin.Core;
using System;

namespace Mearcury.Admin.Test
{
    public static class TestCmd
    {
        public static CmdResult Run(TestOptions opts)
        {
            Console.WriteLine($"|{-1.123456789012345,10:N5}|");

            return CmdResult.NoWait;
        }
    }
}
