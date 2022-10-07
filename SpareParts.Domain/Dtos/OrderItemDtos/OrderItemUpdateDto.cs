namespace SpareParts.Domain.Dtos.OrderItemDtos;

public class OrderItemUpdateDto:UpdateDto<OrderItem>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}