﻿using MiniMarket.Mappers;
using MiniMarket.Models;
using System.Net.Http.Json;

namespace MiniMarket.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public List<Product> Products { get; set; } = new List<Product>() {
            new Product(1, "Apple", 2, 0, "Fresh red apples"),
            new Product(2, "Banana", 1, 0, "Ripe yellow bananas"),
            new Product(3, "Orange", 3, 10, "Juicy oranges with a 10% discount"),
            new Product(4, "Grapes", 4, 50, "Sweet grapes with a 50% discount"),
            new Product(5, "Mango", 5, 0, "Tropical mangoes"),
            new Product(6, "Pineapple", 6, 0, "Fresh pineapples"),
            new Product(7, "Strawberry", 7, 15, "Delicious strawberries with a 15% discount")
        };

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<List<ProductList>> GetProductsAsync()
        {
            List<ProductList> result = await _httpClient.GetFromJsonAsync<List<ProductList>>("api/Product");
            if (result is not null)
            {
                return result;
            }
            return [];
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public async Task<bool> CreateProductAsync(ProductCreateDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", product);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public Task UpdateProductAsync(Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.Description = product.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
            }
            return Task.CompletedTask;
        }
    }
}
