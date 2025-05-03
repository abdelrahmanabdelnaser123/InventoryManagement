using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities
{
    public class InventoryTransaction : Entity<int>
    {
        public InventoryTransaction() : base() { }
        public InventoryTransaction(int id) : base(id) { }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int WarehouseId { get; set; } 
        public Warehouse Warehouse { get; set; }

        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }

        public int Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
