

namespace SpareParts.Domain;

public interface ICategoriesManager
{
    List<ReadCategoryDto> GetAll(string[]? include = null!);
    ReadCategoryDto? GetById(int id);
    ReadCategoryDto Add(AddCategoryDto student);
    bool Update(UpdateCategoryDto student);
    bool Delete(int id);

}