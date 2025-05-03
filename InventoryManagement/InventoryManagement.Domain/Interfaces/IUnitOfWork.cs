using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IWarehouseProductRepository WarehouseProductRepository { get; }
         IInventoryTransactionRepository InventoryTransaction { get; }

        Task<int> SaveChangesAsync();
      
     
    }
}
