using AutoMapper;
using SpareParts.Data;

namespace SpareParts.Domain;

public class CategoriesManager : ICategoriesManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesManager(IUnitOfWork  unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<ReadCategoryDto> GetAll(string[]? include = null!)
    {
        var modelItems = _unitOfWork.Category.GetAll(include);

        return _mapper.Map<List<ReadCategoryDto>>(modelItems);
    }

    public ReadCategoryDto? GetById(int id)
    {
        var modelItem = _unitOfWork.Category.Get(id);
        if (modelItem == null)
            return null;
        return _mapper.Map<ReadCategoryDto>(modelItem);
    }

    public ReadCategoryDto Add(AddCategoryDto dto)
    {
        var modelItem = _mapper.Map<Category>(dto);

        _unitOfWork.Category.Add(modelItem);
        _unitOfWork.Save();

        return _mapper.Map<ReadCategoryDto>(modelItem);
    }

    public bool Update(UpdateCategoryDto dto)
    {
        var modelItem = _unitOfWork.Category.Get(dto.Id);

        if (modelItem is null)
            return false;

        _mapper.Map(dto, modelItem);

        _unitOfWork.Category.Update(modelItem);
        _unitOfWork.Save();

        return true;
    }

    public bool Delete(int id)
    {

        var dbModel = _unitOfWork.Category.Get(id);

        if (dbModel == null)
            return false;

        _unitOfWork.Category.Delete(dbModel);
        _unitOfWork.Save();
        return true;
    }
    
}