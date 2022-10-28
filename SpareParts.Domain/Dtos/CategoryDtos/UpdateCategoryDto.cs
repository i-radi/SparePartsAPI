namespace SpareParts.Domain;

public class UpdateCategoryDto : UpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
}
