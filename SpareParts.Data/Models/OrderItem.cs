namespace SpareParts.Data.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}