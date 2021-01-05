using Mearcury.Entities;
using System;
using System.Runtime.Serialization;

namespace Mearcury.Providers
{
    public class AuthenticationDetails
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string Authority { get; set; }

        public string Salt { get; set; }
    }
}
