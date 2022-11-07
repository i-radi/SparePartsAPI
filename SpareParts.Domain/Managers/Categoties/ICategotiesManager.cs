

namespace SpareParts.Domain;

public interface ICategoriesManager
{
    List<ReadCategoryDto> GetAll(string[]? include = null!);
    ReadCategoryDto? GetById(int id);
    ReadCategoryDto Add(AddCategoryDto model);
    bool Update(UpdateCategoryDto model);
    bool Delete(int id);

}