using AutoMapper;
using SpareParts.Data;

namespace SpareParts.Domain;

public class BrandsManager : IBrandsManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrandsManager(IUnitOfWork  unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<ReadBrandDto> GetAll(string[]? include = null!)
    {
        var modelItems = _unitOfWork.Brand.GetAll(include);

        return _mapper.Map<List<ReadBrandDto>>(modelItems);
    }

    public ReadBrandDto? GetById(int id)
    {
        var modelItem = _unitOfWork.Brand.Get(id);
        if (modelItem == null)
            return null;
        return _mapper.Map<ReadBrandDto>(modelItem);
    }

    public ReadBrandDto Add(AddBrandDto dto)
    {
        var modelItem = _mapper.Map<Brand>(dto);

        _unitOfWork.Brand.Add(modelItem);
        _unitOfWork.Save();

        return _mapper.Map<ReadBrandDto>(modelItem);
    }

    public bool Update(UpdateBrandDto dto)
    {
        var modelItem = _unitOfWork.Brand.Get(dto.Id);

        if (modelItem is null)
            return false;

        _mapper.Map(dto, modelItem);

        _unitOfWork.Brand.Update(modelItem);
        _unitOfWork.Save();

        return true;
    }

    public bool Delete(int id)
    {

        var dbModel = _unitOfWork.Brand.Get(id);

        if (dbModel == null)
            return false;

        _unitOfWork.Brand.Delete(dbModel);
        _unitOfWork.Save();
        return true;
    }
    
}