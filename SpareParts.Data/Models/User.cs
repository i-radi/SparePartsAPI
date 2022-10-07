using Microsoft.AspNetCore.Identity;
using SpareParts.InfraStructure.Enums;

namespace SpareParts.Data.Models;

public class User: IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserType UserType { get; set; }
    public List<Address> Addresses { get; set; }
    public Car Car { get; set; }
    public List<Order> Orders { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}