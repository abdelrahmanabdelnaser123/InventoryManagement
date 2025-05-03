using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Repository;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Services.DTOS;

namespace InventoryManagement.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
       public async Task<Response<List<ProductDto>>> GetAllProductsAsync()
        {
            try
            {
      var  allProduct = await      unitOfWork.ProductRepository
              .GetAll()
              .Select(p => new ProductDto { Id = p.Id, Description = p.Description, Name = p.Name, Price = p.Price }).ToListAsync();
                return new Response<List<ProductDto>>()
                {

                    Status = ResponseStatus.Success,
                    Message = $"{allProduct.Count} Products Successfully fetched",
                    Data = allProduct,

                };
            }
            catch(Exception ex) 
            {
                return new Response<List<ProductDto>>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "Please try again later.",
                    InternalMessage = $"GetAllProductsAsync failed: {ex.Message}",
                    Data = null,
                    SubStatus = 500,

                };

            }




        }
       
        public async Task<Response<ProductDto>> GetProductByIdAsync(int id)
        {



            try
            {
                var product = await unitOfWork.ProductRepository
                 .GetByIdAsync(id);
                if (product == null)
                {
                    return new Response<ProductDto>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = "Product not found",
                        Data = null
                    };
                }

                ProductDto productDto = new ProductDto();
                productDto.Id = product.Id;
                productDto.Description = product.Description;
                productDto.Name = product.Name;
                productDto.Price = product.Price;
              
              

               


                return new Response<ProductDto>
                {
                    Status = ResponseStatus.Success,
                    Message = " Product Successfully fetched",
                    Data = productDto,
                };


            }
            catch (Exception ex)
            {

                return new Response<ProductDto>()
                {

                    Status = ResponseStatus.InternalServerError,
                    Message = "Please try again later.",
                    InternalMessage = $"GetProductsByIdAsync failed: ",
                    Data = null,
                    SubStatus = 500,

                };

            }




        }
    public async    Task<Response<Product>> DeleteProductAsync(int productId)
        {

            try
            {
              Product productFromDb = await unitOfWork.ProductRepository.GetByIdAsync(productId);

                if (productFromDb != null)
                {
                    await unitOfWork.ProductRepository.DeleteAsync(productFromDb);
                    await unitOfWork.SaveChangesAsync();


                    return new Response<Product>()
                    {
                        Status = ResponseStatus.Success,
                        Message = " Products Successfully deleted",



                    };
                }
                else
                {
                    return new Response<Product>()
                {
                        
                      Status = ResponseStatus.NotFound,
                    Message = " Products not found",



                };
                }
            }
            catch(Exception ex) 
            {
                return new Response<Product>()
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = " Products not deleted",



                };

            }
          

        }


        public async Task<Response<ProductDto>> AddProductAsync(CreateProductDto productDt)
        {
            try
            {
                Product product = new Product();
                product.Name = productDt.Name;
                product.Description = productDt.Description;
                product.Price = productDt.Price;
                product.LowStockThreshold = productDt.LowStockThreshold;

        

            await     unitOfWork.ProductRepository.AddAsync(product);

                await unitOfWork.SaveChangesAsync();
                ProductDto productDto = new ProductDto();
                productDto.Id = product.Id;
                productDto.Name = productDt.Name;
                productDto.Description = productDt.Description;
                productDto.Price = productDt.Price;


                return new Response<ProductDto>()
                {
                    Status = ResponseStatus.Success,
                    Message = "product added ",
                    Data = productDto,

                };



            }
            catch (Exception ex) 
            {
                return new Response<ProductDto>()
                {
                  Status = ResponseStatus.InternalServerError,
                    Message = "product not added ",
                    InternalMessage = $" AddProductAsync error: {ex.Message}"

                };
            }

        }

        public async Task<Response<Product>> UpdateProductAsync(int productId, UpdateProductDto productDt)
        {
       Product productFromDb =  await  unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (productFromDb == null)
            {
                return new Response<Product>()
                {
                    Status = ResponseStatus.NotFound,
                    Message = "product not found"
                };
            }

            try
            {


                productFromDb.Name = productDt.Name;
                productFromDb.Description = productDt.Description;
                productFromDb.Price = productDt.Price;
                productFromDb.LowStockThreshold = productDt.LowStockThreshold;
             await   unitOfWork.ProductRepository.UpdateAsync(productFromDb);
              await  unitOfWork.SaveChangesAsync();
                return new Response<Product>()
                {
                    Status = ResponseStatus.Success,
                    Message = "product updated ",
                    Data = productFromDb

                };

            }
            catch(Exception ex) 
            {
                return new Response<Product>()
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "Product update failed",
                    InternalMessage =$"error :{ex.Message}",
                    Data = null

                };
            }
        
        }



    }
}
