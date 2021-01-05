using Mearcury.Entities;
using System;

namespace Mearcury.Providers
{
    public class Provider: TenantEntityBase<Guid>
    {
        public AuthenticationDetails Auth { get; set; }

        public ProviderType Type { get; set; }
    }
}
