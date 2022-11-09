using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SpareParts.Data;

namespace SpareParts.Domain;

public class ShoppingCartsManager : IShoppingCartsManager
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ShoppingCartsManager(UserManager<User> userManager, IUnitOfWork  unitOfWork, IMapper mapper)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public List<ReadShoppingCartDto> GetAll(string[]? include = null)
    {
        var modelItems = _unitOfWork.ShoppingCart.GetAll(new []{ "Product"});

        return _mapper.Map<List<ReadShoppingCartDto>>(modelItems);
    }

    public async Task<UpdateShoppingCartDto> AssignProduct(AssignProductToCartDto dto)
    {
        var modelItem = _mapper.Map<ShoppingCart>(dto);
        string? userId = dto!.UserId.ToString();
        modelItem.User = (await _userManager.FindByIdAsync(userId!))!;
        modelItem.Product = await _unitOfWork.Product.GetAsync(dto.ProductId);
        await _unitOfWork.ShoppingCart.AddAsync(modelItem);
        _unitOfWork.Save();

        return _mapper.Map<UpdateShoppingCartDto>(modelItem);
    }

    public bool Delete(DeleteShoppingCartDto dto)
    {
        var dbModel = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.UserId == dto.UserId && c.ProductId ==dto.ProductId);

        if (dbModel == null)
            return false;

        _unitOfWork.ShoppingCart.Delete(dbModel);
        _unitOfWork.Save();
        return true;
    }

    public bool IncrementCount(UpdateShoppingCartDto dto)
    {
        var modelItem = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId);

        if (modelItem is null)
            return false;

        _unitOfWork.ShoppingCart.IncrementCount(modelItem, 1);
        _unitOfWork.Save();

        return true;
    }

    public bool DecrementCount(UpdateShoppingCartDto dto)
    {
        var modelItem = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId);

        if (modelItem is null)
            return false;

        _unitOfWork.ShoppingCart.DecrementCount(modelItem, 1);
        _unitOfWork.Save();

        return true;
    }

    public bool ChangeCount(ChangeCountShoppingCartDto dto)
    {
        var modelItem = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId);

        if (modelItem is null)
            return false;

        _unitOfWork.ShoppingCart.ChangeCount(modelItem, dto.Count);
        _unitOfWork.Save();

        return true;
    }

}