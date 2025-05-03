using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;

namespace InventoryManagement.Services.Services
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IUnitOfWork unitOfWork;

        public InventoryTransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> RemoveStockAsync(StockDto removeStock)
        {
            WarehouseProduct warehouseProduct = await unitOfWork.WarehouseProductRepository.GetByProductAndWarehouseAsync(removeStock.ProductId, removeStock.WarehouseId);

            if (warehouseProduct == null)
            {
                return new Response<bool>()
                {
                    Status = ResponseStatus.NotFound,
                    Data = false,
                    Message = "product not found"

                };
            }

            if (removeStock.Quantity > warehouseProduct.Quantity)
            {
                return new Response<bool>()
                {
                    Status = ResponseStatus.BadRequest,
                    Data = false,
                    Message = "Quantity bigger than product Quantity"

                };
            }
            warehouseProduct.Quantity -= removeStock.Quantity;

            InventoryTransaction inventoryTransaction = new InventoryTransaction()
            {
                ProductId = removeStock.ProductId,
                WarehouseId = removeStock.WarehouseId,
                Quantity = removeStock.Quantity,
                UserId = removeStock.UserId,
                TransactionType = TransactionType.Remove,
                TransactionDate = DateTime.Now,

            };

            await unitOfWork.InventoryTransaction.AddAsync(inventoryTransaction);
            await unitOfWork.SaveChangesAsync();


            return new Response<bool>
            {
                Status = ResponseStatus.Success,
                Data = true,
                Message = "Stock removed successfully"
            };
        }
        public async Task<Response<bool>> AddStockAsync(StockDto addStock)
        {
            WarehouseProduct warehouseProduct = await unitOfWork.WarehouseProductRepository.GetByProductAndWarehouseAsync(addStock.ProductId, addStock.WarehouseId);

            if (warehouseProduct == null)
            {
                return new Response<bool>()
                {
                    Status = ResponseStatus.NotFound,
                    Data = false,
                    Message = "product not found"

                };
            }

            warehouseProduct.Quantity += addStock.Quantity;

            InventoryTransaction inventoryTransaction = new InventoryTransaction()
            {
                ProductId = addStock.ProductId,
                WarehouseId = addStock.WarehouseId,
                Quantity = addStock.Quantity,
                UserId = addStock.UserId,
                TransactionType = TransactionType.Add,
                TransactionDate = DateTime.Now,

            };
            await unitOfWork.InventoryTransaction.AddAsync(inventoryTransaction);
            await unitOfWork.SaveChangesAsync();


            return new Response<bool>
            {
                Status = ResponseStatus.Success,
                Data = true,
                Message = "Stock added successfully"
            };
        }

        public async Task<Response<bool>> TransferStockAsync(TransferStockDto transferStock)
        {

            var warehouseProductFrom = await unitOfWork.WarehouseProductRepository.GetByProductAndWarehouseAsync(transferStock.ProductId, transferStock.WarehouseId);
            if (warehouseProductFrom == null)
            {
                return new Response<bool>()
                {
                    Status = ResponseStatus.NotFound,
                    Data = false,
                    Message = "Product not found in source warehouse"
                };
            }

            if (transferStock.Quantity > warehouseProductFrom.Quantity)
            {
                return new Response<bool>()
                {
                    Status = ResponseStatus.BadRequest,
                    Data = false,

                };
            }


             var warehouseProductTo = await unitOfWork.WarehouseProductRepository.GetByProductAndWarehouseAsync(transferStock.ProductId, transferStock.ToWarehouseId);

            if (warehouseProductTo == null)
            {

                warehouseProductTo = new WarehouseProduct()
                {
                    ProductId = transferStock.ProductId,
                    WarehouseId = transferStock.ToWarehouseId,
                    Quantity = 0
                };
            }


             warehouseProductFrom.Quantity -= transferStock.Quantity;
            warehouseProductTo.Quantity += transferStock.Quantity;

            InventoryTransaction inventoryTransactionFrom = new InventoryTransaction()
            {


                ProductId = transferStock.ProductId,
                WarehouseId = transferStock.WarehouseId,
                Quantity = transferStock.Quantity,
                UserId = transferStock.UserId,
                TransactionType = TransactionType.Remove,
                TransactionDate = DateTime.Now


            };

            InventoryTransaction inventoryTransactionTo = new InventoryTransaction()
            {
                ProductId = transferStock.ProductId,
                 WarehouseId = transferStock.ToWarehouseId,
                 Quantity = transferStock.Quantity,
                 UserId = transferStock.UserId,
                TransactionType = TransactionType.Add,
                TransactionDate = DateTime.Now


            };


            await unitOfWork.WarehouseProductRepository.UpdateAsync(warehouseProductFrom);
             await unitOfWork.WarehouseProductRepository.UpdateAsync(warehouseProductTo);
              await unitOfWork.InventoryTransaction.AddAsync(inventoryTransactionFrom);
            await unitOfWork.InventoryTransaction.AddAsync(inventoryTransactionTo);
             await unitOfWork.SaveChangesAsync();



            return new Response<bool>()
            {
                Status = ResponseStatus.Success,
                Data = true,
                Message = "Stock transferred successfully"
            };
        }



    }
}
