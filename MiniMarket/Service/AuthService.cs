using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MiniMarket.Models;
using MiniMarket.States;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authState;

        public AuthService(IHttpClientFactory httpClientFactory, IJSRuntime jSRuntime, AuthenticationStateProvider authState)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _jsRuntime = jSRuntime;
            _authState = authState;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO) {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerDTO);

            if (response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginDTO);

            if (response.IsSuccessStatusCode)
            {
                TokenResponse? content = await response.Content.ReadFromJsonAsync<TokenResponse>();

                if (content is not null) {
                    Console.WriteLine("Token recu: " + content.Token);

                    // stocker dans le localStorage (JS)
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", content.Token);

                    // Notifié à l'authentication state manager qu'on est connecté
                    (_authState as AuthState).NotifyStateChanged();

                    return true;
                }
            }
            return false;
        }
    }
}
