﻿namespace OrderService.Models;

public class Order
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Amount { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
}
