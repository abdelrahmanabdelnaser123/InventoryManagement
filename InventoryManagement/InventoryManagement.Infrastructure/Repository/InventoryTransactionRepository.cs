using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Context;

namespace InventoryManagement.Infrastructure.Repository
{
    public class InventoryTransactionRepository : GeneralRepository<InventoryTransaction, int>, IInventoryTransactionRepository
    {
    public    InventoryTransactionRepository(InventoryDbContext _context) : base(_context)
        {

        }
    }
}
