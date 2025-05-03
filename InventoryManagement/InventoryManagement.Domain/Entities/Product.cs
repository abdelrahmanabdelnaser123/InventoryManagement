


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Entities
{
    public class Product : Entity<int>
    {
        public Product() : base()
        { 

         }
        public Product(int id) : base(id)
        {

        }

      public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int LowStockThreshold { get; set; }


        public List<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();
        public string CreatedByUserId { get; set; } 
        public ApplicationUser CreatedByUser { get; set; }
    }
}
