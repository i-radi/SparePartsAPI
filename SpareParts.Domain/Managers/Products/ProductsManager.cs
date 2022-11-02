using AutoMapper;
using SpareParts.Data;

namespace SpareParts.Domain;

public class ProductsManager : IProductsManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsManager(IUnitOfWork  unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<SimpleProductDto> GetAll(string[]? include = null!)
    {
        var modelItems = _unitOfWork.Product.GetAll(include);

        return _mapper.Map<IEnumerable<SimpleProductDto>>(modelItems);
    }

    public List<ReadProductDto> GetAllOwn(Guid userId)
    {
        var modelItems = _unitOfWork.Product.GetAll().Where(p=>p.UserId == userId);

        return _mapper.Map<List<ReadProductDto>>(modelItems);
    }

    public ReadProductDto? GetById(Guid id)
    {
        var modelItem = _unitOfWork.Product.Get(id);
        if (modelItem == null)
            return null;
        return _mapper.Map<ReadProductDto>(modelItem);
    }

    public ReadProductDto Add(AddProductDto dto,User user)
    {
        var modelItem = _mapper.Map<Product>(dto);
        modelItem.Id = Guid.NewGuid();
        modelItem.User = user;
        modelItem.UserId = user.Id;
        modelItem.Brand = _unitOfWork.Brand.Get(dto.BrandId);
        modelItem.Category = _unitOfWork.Category.Get(dto.CategoryId);
        _unitOfWork.Product.Add(modelItem);
        _unitOfWork.Save();

        return _mapper.Map<ReadProductDto>(modelItem);
    }

    public bool Update(UpdateProductDto dto)
    {
        var modelItem = _unitOfWork.Product.Get(dto.Id);

        if (modelItem is null)
            return false;

        modelItem.Brand = _unitOfWork.Brand.Get(dto.BrandId);
        modelItem.Category = _unitOfWork.Category.Get(dto.CategoryId);

        _mapper.Map(dto, modelItem);

        _unitOfWork.Product.Update(modelItem);
        _unitOfWork.Save();

        return true;
    }

    public bool Delete(Guid id)
    {

        var dbModel = _unitOfWork.Product.Get(id);

        if (dbModel == null)
            return false;

        _unitOfWork.Product.Delete(dbModel);
        _unitOfWork.Save();
        return true;
    }
    
}