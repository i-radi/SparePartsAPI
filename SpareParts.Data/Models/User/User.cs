using Microsoft.AspNetCore.Identity;

namespace SpareParts.Data;

public sealed class User : IdentityUser<Guid>
{
    public User()
    {
        CreditCards = new HashSet<CreditCard>();
        OrderHeaders = new HashSet<OrderHeader>();
        Products = new HashSet<Product>();
        Reviews = new HashSet<Review>();
        ShoppingCarts = new HashSet<ShoppingCart>();
    }
    [Required]
    public string Name { get; set; } = String.Empty;
    public string StreetAddress { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;
    public byte[]? ProfilePicture { get; set; }
    public ICollection<CreditCard> CreditCards { get; set; }
    public ICollection<OrderHeader> OrderHeaders { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }

}