using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<Response<List<ProductDto>>> GetAllProduct()
        {
            return await productService.GetAllProductsAsync();

        }
        [HttpGet("{id:int}")]
        public async Task<Response<ProductDto>> GetProductsById(int id)
        {
            return await productService.GetProductByIdAsync(id);
        }
        [HttpDelete("{id:int}")]
        public async Task<Response<Product>> DeleteProduct(int id)
        {
            return await productService.DeleteProductAsync(id);

        }
        [HttpPost]
        public async Task<Response<ProductDto>> AddProduct(CreateProductDto productDt)
        {
            return await productService.AddProductAsync(productDt);

        }
        [HttpPut("{id:int}")]
        public async Task<Response<Product>> UpdateProductAsync(int Id, UpdateProductDto productDt)
        {
         return   await productService.UpdateProductAsync(Id , productDt);
        }
    }
}
