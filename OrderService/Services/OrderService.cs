using OrderService.Models;

namespace OrderService.Services;

public class OrderService
{
    private readonly PaymentService _paymentService;
    private readonly InventoryService _inventoryService;

    public OrderService(PaymentService paymentService, InventoryService inventoryService)
    {
        _paymentService = paymentService;
        _inventoryService = inventoryService;
    }

    public async Task CreateOrder(Order order)
    {
        try
        {
            // Step 1: Reserve inventory
            bool inventoryReserved = await _inventoryService.ReserveStock(order.ProductId, order.Quantity);
            if (!inventoryReserved)
            {

                throw new Exception("Failed to reserve inventory.");
            }

            // Step 2: Process payment
            bool paymentSuccess = await _paymentService.ProcessPayment(order.CustomerId, order.Amount);
            if (!paymentSuccess)
            {
                // Compensation action: Cancel inventory reservation
                await _inventoryService.ReleaseStock(order.ProductId, order.Quantity);
                throw new Exception("Payment failed.");
            }

            // Final Step: Mark order as completed
            order.Status = "Completed";
            Console.WriteLine("Order completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Saga failed: {ex.Message}");
            // Compensation logic or rollback has already been done in each step.
        }
    }
}
