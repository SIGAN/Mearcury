using System.Runtime.Serialization;

namespace Mearcury.Billing
{
    public enum MetricBase
    {
        [EnumMember(Value = "-")]
        Unknown,

        [EnumMember(Value = "U")]
        Unit,

        [EnumMember(Value = "T")]
        Time
    }
}
