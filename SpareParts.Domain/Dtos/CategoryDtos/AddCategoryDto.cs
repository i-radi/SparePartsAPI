namespace SpareParts.Domain;

    public class AddCategoryDto : IDtos
{
    public string Name { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
}
