using Blazored.SessionStorage;
using EjemploBlazorServer.Shared.Modelos;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EjemploBlazorServer.Web.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = await _sessionStorageService.GetItemAsync<string>("userName");

            ClaimsIdentity identity;

            if (userName != null)
            {
                identity = new ClaimsIdentity(new[]
               {
                   new Claim(ClaimTypes.Name, userName),
                }, "apiauth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task MarkUserAsAuthenticated(Usuario usuario)
        {
            await _sessionStorageService.SetItemAsync("accessToken", usuario.AccesToken);

            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.Name, usuario.UserName),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _sessionStorageService.RemoveItemAsync("userName");
            await _sessionStorageService.RemoveItemAsync("accessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}