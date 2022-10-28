namespace SpareParts.Domain;

    public class ReadCategoryDto : ReadDto
    {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
}
