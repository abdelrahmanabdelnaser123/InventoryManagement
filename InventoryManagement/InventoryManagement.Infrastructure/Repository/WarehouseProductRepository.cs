using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repository
{
    public class WarehouseProductRepository : GeneralRepository<WarehouseProduct, int> , IWarehouseProductRepository
    {
        private readonly InventoryDbContext context;

        public WarehouseProductRepository(InventoryDbContext _context) : base(_context)
    {
            context = _context;
        }
      public async  Task<WarehouseProduct> GetByProductAndWarehouseAsync(int productId, int warehouseId)
        {
       return   await  context.WarehouseProducts
                .FirstOrDefaultAsync(w => w.ProductId == productId & w.WarehouseId == warehouseId);


        }
    }
}

