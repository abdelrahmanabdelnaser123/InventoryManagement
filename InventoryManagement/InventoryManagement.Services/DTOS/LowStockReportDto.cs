using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services.DTOS
{
    public class LowStockReportDto
    {
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
