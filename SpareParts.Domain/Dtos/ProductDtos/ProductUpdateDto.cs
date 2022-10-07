namespace SpareParts.Domain.Dtos.ProductDtos;

public class ProductUpdateDto:UpdateDto<Product>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Manufacture { get; set; }
    public string ModelNumber { get; set; }
    public string Details { get; set; }
    public string ImgPath { get; set; }
    public Category Category { get; set; }
    public Car Car { get; set; }
    public int Count { get; set; }


}