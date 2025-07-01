using Microsoft.AspNetCore.Components;
using MiniMarket.Models;
using MiniMarket.Service;

namespace MiniMarket.Pages
{
    public partial class Products
    {
        [Inject]
        public ProductService ProductService { get; set; } = null!;
        [Inject]
        public CartService CartService { get; set; } = null!;

        public List<ProductList>? ProductList { get; set; } = null;
        public Dictionary<int, int> Quantities { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            ProductList = await ProductService.GetProductsAsync();
            if (ProductList != null)
            {
                Console.WriteLine("foreach");
                foreach (var product in ProductList)
                {
                    Quantities[product.Id] = 1; // valeur par défaut
                }
            }
        }

        private async Task DeleteProduct(int id)
        {
            if (ProductList != null)
            {
                await ProductService.DeleteProductAsync(id);
                ProductList = await ProductService.GetProductsAsync();
            }
        }

        private async Task AddToCart(int productId)
        {
            ProductList? p = ProductList?.FirstOrDefault(x => x.Id == productId);
            if (p is null)
            {
                return; // Produit non trouvé
            }

            CartProduct cp = new CartProduct
            {
                Product = p,
                Quantity = Quantities.ContainsKey(productId) ? Quantities[productId] : 1,
            };
            await CartService.AddToCartAsync(cp);
        }
    }
}
