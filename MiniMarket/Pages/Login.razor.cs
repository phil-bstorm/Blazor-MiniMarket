using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class Login
    {
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public LoginDTO Model { get; set; } = new LoginDTO();

        public async Task OnSubmit()
        {
            bool success = await AuthService.LoginAsync(Model);
            if (success) {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
