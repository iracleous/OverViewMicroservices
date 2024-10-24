namespace OrderService.Services;

public class InventoryService
{
    public async Task<bool> ReserveStock(int productId, int quantity)
    {
        // Simulate reserving stock
        Console.WriteLine("Reserving inventory...");
        await Task.Delay(1000); // Simulate async call

        // Simulate success/failure scenario
        bool inventoryReserved = new Random().Next(0, 2) == 1;
        if (!inventoryReserved)
        {
            Console.WriteLine("Failed to reserve inventory.");
            return false;
        }

        Console.WriteLine("Inventory reserved successfully.");
        return true;
    }

    public async Task ReleaseStock(int productId, int quantity)
    {
        // Simulate releasing reserved stock
        Console.WriteLine("Releasing reserved inventory...");
        await Task.Delay(1000); // Simulate async call

        Console.WriteLine("Inventory released.");
    }
}
