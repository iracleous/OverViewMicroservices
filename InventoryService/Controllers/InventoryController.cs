using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private static List<Inventory> inventories = new List<Inventory>
    {
        new Inventory { ProductId = 1, Quantity = 10 },
        new Inventory { ProductId = 2, Quantity = 20 }
    };

    [HttpGet("{productId}")]
    public IActionResult GetInventory(int productId)
    {
        var inventory = inventories.FirstOrDefault(i => i.ProductId == productId);
        if (inventory == null)
            return NotFound();
        return Ok(inventory);
    }

    [HttpPost]
    public IActionResult UpdateInventory(int productId, int quantity)
    {
        var inventory = inventories.FirstOrDefault(i => i.ProductId == productId);
        if (inventory == null)
            return NotFound();

        inventory.Quantity -= quantity;
        return Ok(inventory);
    }
}
