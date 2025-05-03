using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Context;
using InventoryManagement.Infrastructure.Repository;

namespace InventoryManagement.Infrastructure.Repository
{
   public class ProductRepository: GeneralRepository<Product,int> ,IProductRepository
    {
        public ProductRepository(InventoryDbContext _context) : base(_context)
        {

        }

    }
}  

