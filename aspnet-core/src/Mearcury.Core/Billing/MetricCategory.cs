using System.Runtime.Serialization;

namespace Mearcury.Billing
{
    public enum MetricCategory
    {
        [EnumMember(Value = "-")]
        Unknown,

        [EnumMember(Value = "N")]
        Networking,

        [EnumMember(Value = "P")]
        Processing,

        [EnumMember(Value = "S")]
        Storage,

        [EnumMember(Value = "M")]
        Memory,

        [EnumMember(Value = "T")]
        Throughput,

        [EnumMember(Value = "L")]
        Load
    }
}
