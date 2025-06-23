using MiniMarket.Models;

namespace MiniMarket.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProduct(this ProductCreateDTO productCreateDTO)
        {
            return new Product
            {
                Name = productCreateDTO.Name,
                Price = productCreateDTO.Price,
                Discount = productCreateDTO.Discount,
                Description = productCreateDTO.Description
            };
        }
    }
}
