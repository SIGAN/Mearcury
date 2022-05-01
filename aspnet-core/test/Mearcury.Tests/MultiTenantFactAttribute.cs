using Xunit;

namespace Mearcury.Tests
{
    public sealed class MultiTenantFactAttribute : FactAttribute
    {
        public MultiTenantFactAttribute()
        {
#pragma warning disable CS0162 // Unreachable code detected
            if (!MearcuryConsts.MultiTenancyEnabled)
            {
                Skip = "MultiTenancy is disabled.";
            }
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}
