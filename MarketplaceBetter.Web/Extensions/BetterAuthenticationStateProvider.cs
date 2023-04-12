using Blazored.SessionStorage;
using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceBetter.Web.Extensions
{
    public class BetterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _localStorageService;
        private readonly IUserService _userService;
        private const string SESSION_KEY = "a$3rU72D";
        private const string LOGIN = "BetterAdmin";

        public BetterAuthenticationStateProvider(ISessionStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string login = await _localStorageService.GetItemAsStringAsync(SESSION_KEY);

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(login) && login == LOGIN)
            {
                identity = new(new[] { new Claim(ClaimTypes.Name, login) }, "Password");
            }

            ClaimsPrincipal user = new(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void LoginUser()
        {
            _localStorageService.SetItemAsStringAsync(SESSION_KEY, LOGIN);
        }
    }
}
