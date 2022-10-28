
namespace SpareParts.Data;

public sealed class ProductDemo 
{


    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = String.Empty;
    public string Manufacture { get; set; } = String.Empty;
    public string ModelNumber { get; set; } = String.Empty;
    public string Details { get; set; } = String.Empty;
    public string ImgPath { get; set; } = String.Empty;
    public double Price { get; set; } = 1;
    public int Count { get; set; }
    public byte[]? ProductPicture { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public Guid UserId { get; set; }
}