using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.Response;

namespace InventoryManagement.Services.IServices
{
    public interface IInventoryTransactionService
    {
      
        Task<Response<bool>> RemoveStockAsync(StockDto removeStock);
        Task<Response<bool>> AddStockAsync(StockDto addStock);
        Task<Response<bool>> TransferStockAsync(TransferStockDto transferStock);
          
    }
}
