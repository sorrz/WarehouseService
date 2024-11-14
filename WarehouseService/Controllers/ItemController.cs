using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.Models;
using WarehouseService.Services;

namespace WarehouseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly IItemRepository _itemRepository;

    public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
    {
        _logger = logger;
        _itemRepository = itemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Item>>> GetAllItemsAsync()
    {
        var result = await _itemRepository.GetItemsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItemByIdAsync(int id)
    {
        var result = await _itemRepository.GetItemByIdAsync(id);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPut("UpdateItem")]
    public async Task<ActionResult<Item>> UpdateItemAsync(int id, Item updatedItem)
    {
        var result = await _itemRepository.UpdateItemAsync(id, updatedItem);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("DeleteItem")]
    public async Task<string> DeleteItemAsync(int id)
    {
        var result = await _itemRepository.DeleteItemAsync(id);
        return result.Value?.Message;
    }
}