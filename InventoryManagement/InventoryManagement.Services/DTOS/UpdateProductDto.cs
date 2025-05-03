using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services.DTOS
{
    public class UpdateProductDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
