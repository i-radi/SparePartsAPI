using SpareParts.InfraStructure.Enums;

namespace SpareParts.Data.Models;

public class ProductReadDto
{
    public int Id { get; set; }
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