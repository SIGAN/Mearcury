using Mearcury.Entities;
using System;
using System.Runtime.Serialization;

namespace Mearcury.Providers
{

    public enum ProviderType
    {
        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "Azure")]
        Azure,

        [EnumMember(Value = "Amazon")]
        Amazon,

        [EnumMember(Value = "Google")]
        Google
    }
}
