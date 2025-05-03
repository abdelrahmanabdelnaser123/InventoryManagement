using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces
{
    public interface IWarehouseProductRepository : IGeneralRepository<WarehouseProduct, int>
    {
        Task<WarehouseProduct?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
    }
}
