using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Net.Http;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static List<Order> orders = new List<Order>();

    private readonly HttpClient _httpClientProductService;
    private readonly HttpClient _httpClientInventoryService;


    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClientProductService = httpClientFactory.CreateClient("ProductServiceClient");
        _httpClientInventoryService = httpClientFactory.CreateClient("InventoryServiceClient");
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
 
        //communication code

        // Check product exists in ProductService
        var productResponse = await _httpClientProductService.GetAsync($"/api/product/{order.ProductId}");
        if (!productResponse.IsSuccessStatusCode)
        {
            return BadRequest("Product not found.");
        }

        // Call InventoryService to reduce quantity
        var inventoryResponse = await _httpClientInventoryService.PostAsync($"/api/inventory?productId={order.ProductId}&quantity={order.Quantity}", null);
        if (!inventoryResponse.IsSuccessStatusCode)
        {
            return BadRequest("Insufficient inventory.");
        }

        order.OrderId = orders.Count + 1;
        order.OrderDate = DateTime.Now;
        orders.Add(order);
        return Ok(order);
    }
}
