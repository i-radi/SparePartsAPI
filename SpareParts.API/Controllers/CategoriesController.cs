
namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesManager _categoriesManager;

    public CategoriesController(ICategoriesManager categoriesManager)
    {
        _categoriesManager = categoriesManager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UpdateCategoryDto>> GetAll()
    {
        return Ok(_categoriesManager.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<UpdateCategoryDto> GetById(int id)
    {
        var dto = _categoriesManager.GetById(id);
        if (dto is null)
            return BadRequest("Not Found Category");
        return Ok(dto);
    }

    [HttpGet]
    [Route("Count")]
    public ActionResult<int> GetCount()
    {
        return Ok(_categoriesManager.GetAll().Count);
    }

    [HttpPost]
    public ActionResult<UpdateCategoryDto> CreateCategory(AddCategoryDto dto)
    {
        return Ok(_categoriesManager.Add(dto));
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCategory(int id, UpdateCategoryDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Id not matched");
        }
        if (!_categoriesManager.Update(dto))
        {
            return BadRequest("Not Updated");
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        if (!_categoriesManager.Delete(id))
        {
            return BadRequest("Not Deleted");
        }
        return NoContent();
    }
    
}