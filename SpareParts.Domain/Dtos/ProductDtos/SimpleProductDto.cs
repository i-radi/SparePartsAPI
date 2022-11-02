namespace SpareParts.Domain;

    public class SimpleProductDto : ReadDto
    {
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string ImgPath { get; set; } = String.Empty;
    public double Price { get; set; } = 1;
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
}
