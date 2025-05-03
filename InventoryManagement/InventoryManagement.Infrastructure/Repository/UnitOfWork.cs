using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repository
{
    public class UnitOfWork:IUnitOfWork , IDisposable

    {
       private IProductRepository _ProductRepository;
        private IInventoryTransactionRepository inventoryTransaction;
        private  IWarehouseProductRepository _warehouseProductRepositor;
        private readonly InventoryDbContext _context;
       public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }
        public IWarehouseProductRepository WarehouseProductRepository
        {
            get
            {
                if (_warehouseProductRepositor == null)
                {
                    _warehouseProductRepositor = new WarehouseProductRepository(_context);
                }
                return _warehouseProductRepositor;
            }
        }
        public IInventoryTransactionRepository InventoryTransaction
        {
            get
            {
                if (inventoryTransaction == null)
                {
                    inventoryTransaction = new InventoryTransactionRepository(_context);
                }
                return inventoryTransaction;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                {
                    _ProductRepository = new ProductRepository(_context);
                }
                return _ProductRepository;
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
