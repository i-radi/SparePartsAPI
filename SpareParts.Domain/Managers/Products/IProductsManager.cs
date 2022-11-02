

using SpareParts.Data;

namespace SpareParts.Domain;

public interface IProductsManager
{
    IEnumerable<SimpleProductDto> GetAll(string[]? include = null!);
    List<ReadProductDto> GetAllOwn(Guid userId);
    ReadProductDto? GetById(Guid id);
    ReadProductDto Add(AddProductDto dto, User user);
    bool Update(UpdateProductDto dto);
    bool Delete(Guid id);

}