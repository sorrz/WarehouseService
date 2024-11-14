using Microsoft.AspNetCore.Mvc;
using WarehouseService.Controllers;
using WarehouseService.Models;

namespace WarehouseService.Services;

public interface IItemRepository
{
    Task<List<Item>> GetItemsAsync();
    Task<Item> GetItemByIdAsync(int id);
    Task<Item> UpdateItemAsync(int id, Item updatedItem);
    Task<ActionResult<DeleteResult>> DeleteItemAsync(int id);

}