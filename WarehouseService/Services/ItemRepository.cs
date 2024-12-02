using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseService.Controllers;
using WarehouseService.Database;
using WarehouseService.Models;

namespace WarehouseService.Services;

public class ItemRepository : IItemRepository
{
    private readonly ItemDbContext _context;
    
    public ItemRepository(ItemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        var result = await _context.Products.ToListAsync();
        return result.Count == 0 ? new List<Item>() : result;
    }

    public async Task<Item> GetItemByIdAsync(int id)
    {
        // Fetch a single item by its ID. Return null if not found.
        var item = await _context.Products.FindAsync(id);
        return item ?? null;
    }

    public async Task<Item> UpdateItemAsync(int id, Item updatedItem)
    {
        // Find the item by its ID.
        var item = await _context.Products.FindAsync(id);
        if (item != null)
        {
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.Details = updatedItem.Details;
            item.PictureUrl = updatedItem.PictureUrl;

            // Save changes
            await _context.SaveChangesAsync();
            return item;
        }

        // Update properties of the item as needed
        // If the item doesn't exist, handle the case accordingly (e.g., throw an exception or return null).
        return null;
    }

    public async Task<ActionResult<DeleteResult>> DeleteItemAsync(int id)
    {
        // Find the item by its ID
        var item = await _context.Products.FindAsync(id);
        if (item == null)
        {
            return new DeleteResult(2); // Item not found
        }

        // Remove the item from the context
        _context.Products.Remove(item);
        await _context.SaveChangesAsync();

        return new DeleteResult(1); // Item deleted successfully
    }
}

public class DeleteResult
{
    public int Id { get; set; }
    public string? Message { get; set; }

    public DeleteResult(int id)
    {
        Message = id switch
        {
            1 => "Item Deleted",
            2 => "Item Not Found",
            _ => Message
        };
    }
}

