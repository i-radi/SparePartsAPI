namespace SpareParts.Domain.Dtos.OrderDtos;

public class OrderCreateDto:CreateDto<Product>
{
    public string OrderNumber { get; set; }
    public User User { get; set; }
    public OrderStatus Status { get; set; }
    public double TotalOrderPrice { get; set; }
}