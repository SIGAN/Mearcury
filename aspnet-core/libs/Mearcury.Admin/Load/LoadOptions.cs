using CommandLine;
using Mearcury.Admin.Core;

namespace Mearcury.Admin.Load
{
    [Verb("load", HelpText = "Loads resources and billing information.")]
    public class LoadOptions : CoreOptions
    {
    }
}
