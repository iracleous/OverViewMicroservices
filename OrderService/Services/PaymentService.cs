namespace OrderService.Services;

public class PaymentService
{
    public async Task<bool> ProcessPayment(int customerId, decimal amount)
    {
        // Simulate payment processing
        Console.WriteLine("Processing payment...");
        await Task.Delay(1000); // Simulate async call

        // Simulate failure scenario (e.g., insufficient funds)
        bool paymentSuccess = new Random().Next(0, 2) == 1;
        if (!paymentSuccess)
        {
            Console.WriteLine("Payment failed!");
            return false;
        }

        Console.WriteLine("Payment successful.");
        return true;
    }

    // Compensation Logic: Refund the payment if something fails in the saga
    public async Task RefundPayment(int customerId, decimal amount)
    {
        Console.WriteLine("Refunding payment...");
        await Task.Delay(1000); // Simulate async call

        Console.WriteLine("Payment refunded.");
    }
}
