using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Mearcury.Azure
{
    public class AzureIdentity
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUrl;

        public AzureIdentity(string redirectUrl, string clientId)
        {
            if (redirectUrl == null)
                throw new ArgumentNullException(nameof(redirectUrl));

            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId));

            _redirectUrl = redirectUrl;
            _clientId = clientId;
        }

        public AzureIdentity(string redirectUrl, string clientId, string clientSecret)
            : this(redirectUrl, clientId)
        {
            if (clientSecret == null)
                throw new ArgumentNullException(nameof(clientSecret));

            _clientSecret = clientSecret;
        }

        private Task<AuthenticationResult> Authenticate(string authority, string resource, string scope)
        {
            var authCtx = new AuthenticationContext(authority, TokenCache.DefaultShared);
            return
                _clientSecret != null
                ? authCtx.AcquireTokenAsync(resource, new ClientCredential(_clientId, _clientSecret))
                : authCtx.AcquireTokenAsync(resource, _clientId, new Uri(_redirectUrl), new PlatformParameters());
        }

        public Task<string> GetTokenAsync(string authority, string resource, string scope = null)
        {
            return
                Authenticate(authority, resource, scope)
                .ContinueWith(tokenTask =>
                {
                    if (tokenTask.Result == null)
                        throw new InvalidOperationException("Failed to obtain the JWT token for " + resource);

                    return tokenTask.Result.AccessToken;
                });
        }
    }
}
