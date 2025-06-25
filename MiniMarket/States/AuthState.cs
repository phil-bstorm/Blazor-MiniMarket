using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniMarket.States
{
    // il faut installer le package Microsoft.AspNetCore.Components.Authorization
    // Attention à la version si vous êtes en .NET 8
    public class AuthState : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public AuthState(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

                if (!string.IsNullOrEmpty(token))
                {
                    // il faut installer le NuGet package System.IdentityModel.Tokens.Jwt
                    var jwt = new JwtSecurityToken(token);
                    ClaimsIdentity currentUserIdentity = new ClaimsIdentity(jwt.Claims, "JwtAuth");
                    return new AuthenticationState(new ClaimsPrincipal(currentUserIdentity));
                }
            }
            catch (Exception ex)
            {
                // TODO
            }
            return new AuthenticationState(new ClaimsPrincipal());
        }

        public void NotifyStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
