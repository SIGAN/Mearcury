using Microsoft.Rest;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Mearcury.Azure.Core
{
    public class AzureAuthenticationServiceCredentials : ServiceClientCredentials
    {
        private readonly AzureIdentity _authentication;
        private readonly string _resource;
        private readonly string _authority;

        public AzureAuthenticationServiceCredentials(AzureIdentity authentication, string authority, string resource)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            if (string.IsNullOrWhiteSpace(authority))
                throw new ArgumentException($"{nameof(authority)} is required!", nameof(authority));

            if (string.IsNullOrWhiteSpace(resource))
                throw new ArgumentException($"{nameof(resource)} is required!", nameof(resource));

            _authentication = authentication;
            _resource = resource;
            _authority = authority;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _authentication
                .GetTokenAsync(_authority, _resource, null)
                .ContinueWith(tokenTask =>
                {
                    if (tokenTask.Result == null)
                        throw new InvalidOperationException("Failed to obtain the JWT token for " + _resource);

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenTask.Result);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });
        }
    }
}
