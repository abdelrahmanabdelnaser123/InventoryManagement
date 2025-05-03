using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.Response;
using InventoryManagement.Services.DTOS;

namespace InventoryManagement.Services.IServices
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> GetAllProductsAsync();
        Task<Response<ProductDto>> GetProductByIdAsync(int id);
     
        Task<Response<Product>> DeleteProductAsync(int productId);
        Task<Response<ProductDto>> AddProductAsync(CreateProductDto productDt);
        Task<Response<Product>> UpdateProductAsync(int productId, UpdateProductDto productDt);
    }

}