using Microsoft.AspNetCore.Components;
using MiniMarket.Mappers;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class ProductUpdate
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public ProductCreateDTO Model { get; set; } = null!;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync() {
            Product? product = await ProductService.GetProductByIdAsync(Id);

            if(product == null)
            {
                Console.WriteLine("Product not found...");
                NavigationManager.NavigateTo("/product");
                return;
            }

            Model = new ProductCreateDTO
            {
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                Description = product.Description
            };
        }

        public async Task OnSubmit()
        {
            Product p = Model.ToProduct();
            p.Id = Id;
            await ProductService.UpdateProductAsync(p);

            NavigationManager.NavigateTo("/product");
        }
    }
}
