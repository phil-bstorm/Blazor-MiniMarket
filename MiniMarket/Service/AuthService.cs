using Microsoft.JSInterop;
using MiniMarket.Models;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jSRuntime;
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

                    return true;
                }
            }
            return false;
        }
    }
}
