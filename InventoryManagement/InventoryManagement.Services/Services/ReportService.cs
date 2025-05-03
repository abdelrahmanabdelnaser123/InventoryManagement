using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<List<LowStockReportDto>>> GetLowStockReportAsync()
        {
            var lowStockProducts = await unitOfWork.WarehouseProductRepository
                .GetAll()
                .Where(w => w.Quantity < w.Product.LowStockThreshold)
                .Select(w => new LowStockReportDto
                {
                    ProductName = w.Product.Name,
                    WarehouseName = w.Warehouse.Name,
                    Quantity = w.Quantity,
                    LowStockThreshold = w.Product.LowStockThreshold,

                })
                .ToListAsync();

                if (lowStockProducts.Count == 0)
            {
                return new Response<List<LowStockReportDto>>
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Not found  products ",
                    Data = null
                };
            }

            return new Response<List<LowStockReportDto>>
            {
                Status = ResponseStatus.Success,
          
                Data = lowStockProducts
            };

        }
    }
}
