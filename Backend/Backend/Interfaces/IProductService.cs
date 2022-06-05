using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IProductService
    {
        ProductDto AddProduct(ProductDto newProductDto);
        List<ProductDto> GetProducts();
        ProductDto GetById(int id);
        ProductDto UpdateProduct(int id, ProductDto newProductData);
        void DeleteProduct(int id);
    }
}
