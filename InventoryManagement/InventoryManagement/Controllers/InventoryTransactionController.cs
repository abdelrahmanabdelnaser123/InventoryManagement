using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactionService inventoryTransaction;

        public InventoryTransactionController(IInventoryTransactionService inventoryTransaction)
        {
            this.inventoryTransaction = inventoryTransaction;
        }

        [HttpDelete("remove")]

        public async    Task<Response<bool>> RemoveStockAsync(StockDto removeStock)
        {
           return await  inventoryTransaction.RemoveStockAsync(removeStock);

        }
        [HttpPost("add")]
        public async Task<Response<bool>> addStockAsync(StockDto addStock)
        {
            return await inventoryTransaction.AddStockAsync(addStock);

        }

        [HttpPut("transfer")]
        public async  Task<Response<bool>> TransferStockAsync(TransferStockDto transferStock)
        {
             return   await   inventoryTransaction.TransferStockAsync(transferStock);
        }

    }
}
