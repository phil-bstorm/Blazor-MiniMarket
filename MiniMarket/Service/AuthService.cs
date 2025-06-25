using MiniMarket.Models;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO) {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerDTO);

            if (response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }
    }
}
