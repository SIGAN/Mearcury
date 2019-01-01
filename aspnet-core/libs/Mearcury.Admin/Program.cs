using CommandLine;
using Mearcury.Admin.Core;
using Mearcury.Admin.Load;
using Mearcury.Admin.Test;
using System;

namespace Mearcury.Admin
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var result =
                Parser.Default
                    .ParseArguments<LoadOptions, TestOptions>(args)
                    .MapResult<LoadOptions, TestOptions, CmdResult>(
                        LoadCmd.Run,
                        TestCmd.Run,
                        _ => null
                    );

            if (result?.Wait ?? true)
                Console.ReadLine();
        }
    }
}
