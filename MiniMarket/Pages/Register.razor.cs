using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class Register
    {
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        public RegisterDTO Model { get; set; } = new RegisterDTO();

        public async Task OnSubmit() {
            await AuthService.RegisterAsync(Model);
        }
    }
}
