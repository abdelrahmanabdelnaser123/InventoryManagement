using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace InventoryManagement.Domain.Entities
{
    public class WarehouseProduct : Entity<int>
    {
        public WarehouseProduct() : base() { }
        public WarehouseProduct(int id) : base(id) { }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public string CreatedByUserId { get; set; } 
        public ApplicationUser CreatedByUser { get; set; }
    }
}
