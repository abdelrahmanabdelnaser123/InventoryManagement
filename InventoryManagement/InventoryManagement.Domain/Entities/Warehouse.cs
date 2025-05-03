using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Entities
{
    public class Warehouse : Entity<int>
    {
        public Warehouse() : base() 
        {
        }
        public Warehouse(int id) : base(id)
        { 
        }

        public string Name { get; set; }
        public string Location { get; set; }
        public List<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();
        public string CreatedByUserId { get; set; } 
        public ApplicationUser CreatedByUser { get; set; }
    }
}
