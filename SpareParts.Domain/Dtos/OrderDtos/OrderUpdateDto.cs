﻿namespace SpareParts.Domain.Dtos.OrderDtos;

public class OrderUpdateDto:UpdateDto<Product>
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public User User { get; set; }
    public OrderStatus Status { get; set; }
    public double TotalOrderPrice { get; set; }
}