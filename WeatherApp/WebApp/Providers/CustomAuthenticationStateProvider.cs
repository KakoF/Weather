using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace WebApp.Providers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var result = await _sessionStorage.GetAsync<string>("authToken");

            if (!result.Success || string.IsNullOrEmpty(result.Value))
                return new AuthenticationState(_anonymous);

            var token = result.Value;

            var identity = new ClaimsIdentity(new[]
            {
                new Claim("AuthToken", token)
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

		public async Task MarkUserAsAuthenticatedAsync(string token)
		{
			var identity = new ClaimsIdentity(new[]
		    {
			    new Claim("AuthToken", token)
            }, "apiauth_type");

			var user = new ClaimsPrincipal(identity);

			await _sessionStorage.SetAsync("authToken", token);
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
		}

		public void MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
