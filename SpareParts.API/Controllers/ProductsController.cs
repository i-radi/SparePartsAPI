
using SpareParts.InfraStructure.Helpers;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IProductsManager _productsManager;

    public ProductsController(UserManager<User> userManager, IProductsManager productsManager)
    {
        _userManager = userManager;
        _productsManager = productsManager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SimpleProductDto>> GetAll() => Ok(_productsManager.GetAll());
    

    [HttpGet("ownProduct")]
    [Authorize(Policy = "Vendor")]
    public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetAllOwn()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        return Ok(_productsManager.GetAllOwn(currentUser!.Id));
    }

    [HttpGet("withPagination")]
    public ActionResult<IEnumerable<SimpleProductDto>> GetAllWithPagination(int pageNumber,int productsPerPage)
    {
        var result = PaginatedList<SimpleProductDto>.Create(_productsManager.GetAll(), pageNumber, productsPerPage);
        return Ok(result);
    }

    [HttpGet("withBrand")]
    public ActionResult<IEnumerable<SimpleProductDto>> GetAllWithBrand(int brandId, int pageNumber, int productsPerPage)
    {
        var dtos = _productsManager.GetAll().Where(p => p.BrandId == brandId);
        var result = PaginatedList<SimpleProductDto>.Create(dtos, pageNumber, productsPerPage);
        return Ok(result);
    }

    [HttpGet("withCategory")]
    public ActionResult<IEnumerable<SimpleProductDto>> GetAllWithCategory(int categoryId, int pageNumber, int productsPerPage)
    {
        var dtos = _productsManager.GetAll().Where(p => p.CategoryId == categoryId);
        var result = PaginatedList<SimpleProductDto>.Create(dtos, pageNumber, productsPerPage);
        return Ok(result);
    }

    [HttpGet("withPrice")]
    public ActionResult<IEnumerable<SimpleProductDto>> GetAllWithPrice(double minPrice, double maxPrice, int pageNumber, int productsPerPage)
    {
        var dtos = _productsManager.GetAll().Where(p => p.Price <= maxPrice && p.Price >= minPrice);
        var result = PaginatedList<SimpleProductDto>.Create(dtos, pageNumber, productsPerPage);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ReadProductDto> GetProductById(Guid id)
    {
        var dto = _productsManager.GetById(id);
        if (dto is null)
            return BadRequest("Not Found Product");
        return Ok(dto);
    }

    [HttpGet]
    [Route("Count")]
    public ActionResult<int> GetCountProduct()
    {
        return Ok(_productsManager.GetAll().ToList().Count);
    }

    [HttpPost]
    [Authorize(Policy = "Vendor")]
    public async Task<ActionResult<ReadProductDto>> CreateProduct(AddProductDto dto)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        return Ok(_productsManager.Add(dto, currentUser!));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Vendor")]
    public ActionResult UpdateProduct(Guid id, UpdateProductDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Id not matched");
        }
        if (!_productsManager.Update(dto))
        {
            return BadRequest("Not Updated");
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "Vendor")]
    public ActionResult DeleteProduct(Guid id)
    {
        if (!_productsManager.Delete(id))
        {
            return BadRequest("Not Deleted");
        }
        return NoContent();
    }
    
}