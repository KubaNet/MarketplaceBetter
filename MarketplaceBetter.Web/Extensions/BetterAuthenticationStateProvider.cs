using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceBetter.Web.Extensions
{
    public class BetterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private const string SESSION_KEY = "a$3rU72D";
        private const string USER_NAME = "BetterAdmin";

        public BetterAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string userName = await _localStorageService.GetItemAsStringAsync(SESSION_KEY);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(userName) && userName == USER_NAME)
            {
                identity = new(new[] { new Claim(ClaimTypes.Name, userName) }, "Password");
            }

            ClaimsPrincipal user = new(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void LoginUser()
        {
            _localStorageService.SetItemAsStringAsync(SESSION_KEY, USER_NAME);
        }
    }
}
