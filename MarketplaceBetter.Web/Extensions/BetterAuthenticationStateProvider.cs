using MarketplaceBetter.Services.Specialized.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketplaceBetter.Web.Extensions
{
    public class BetterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;

        public BetterAuthenticationStateProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string login = _userService.GetCurrentUserLogin();

            ClaimsIdentity identity = new ClaimsIdentity();

            if (!string.IsNullOrWhiteSpace(login))
            {
                identity = new(new[] { new Claim(ClaimTypes.Name, login) }, "Password");
            }

            ClaimsPrincipal user = new(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void LoginUser(string login)
        {
            _userService.Login(login);
        }
    }
}
