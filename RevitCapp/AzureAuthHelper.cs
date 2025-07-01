using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace RevitCapp
{
    public class AzureAuthHelper
    {
        private static AzureAuthHelper _instance;
        private static readonly object _lock = new object();

        private readonly IPublicClientApplication _app;
        private readonly string[] _scopes;
        private AuthenticationResult _authResult;

        // Make the constructor private
        private AzureAuthHelper(string clientId, string tenantId, string[] scopes)
        {
            _scopes = scopes;
            _app = PublicClientApplicationBuilder.Create(clientId)
                .WithAuthority($"https://login.microsoftonline.com/{tenantId}/v2.0")
                .WithRedirectUri("http://localhost:12345")
                .Build();
            var storageProperties = new StorageCreationPropertiesBuilder(
                    "tokencache.bin",
                    AppDomain.CurrentDomain.BaseDirectory)
                .Build();

            var cacheHelper = MsalCacheHelper.CreateAsync(storageProperties).GetAwaiter().GetResult();
            cacheHelper.RegisterCache(_app.UserTokenCache);



        }

        // Singleton initializer
        public static AzureAuthHelper Init(string clientId, string tenantId, string[] scopes)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AzureAuthHelper(clientId, tenantId, scopes);
                }
            }
            return _instance;
        }

        // Accessor
        public static AzureAuthHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("AzureAuthHelper not initialized. Call Init() first.");
                }
                return _instance;
            }
        }

        // Gets the access token
        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var accounts = await _app.GetAccountsAsync();
                _authResult = await _app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                                        .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                _authResult = await _app.AcquireTokenInteractive(_scopes)
                                        .WithPrompt(Prompt.SelectAccount)
                                        .ExecuteAsync();
            }

            return _authResult.AccessToken;
        }

        public string GetSignedInUser()
        {
            return _authResult?.Account?.Username;
        }
        public async Task SignOutAsync()
        {
            var accounts = await _app.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await _app.RemoveAsync(account);
            }
        }
    }
}
