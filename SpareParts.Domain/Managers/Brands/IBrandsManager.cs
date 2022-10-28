

namespace SpareParts.Domain;

public interface IBrandsManager
{
    List<ReadBrandDto> GetAll(string[]? include = null!);
    ReadBrandDto? GetById(int id);
    ReadBrandDto Add(AddBrandDto student);
    bool Update(UpdateBrandDto student);
    bool Delete(int id);

}