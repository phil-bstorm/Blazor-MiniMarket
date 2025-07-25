﻿using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class ProductCreate
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public ProductCreateDTO Model { get; set; } = new ProductCreateDTO();

        public async Task OnSubmit()
        {
            await ProductService.CreateProductAsync(Model);

            NavigationManager.NavigateTo("/product");
        }
    }
}
