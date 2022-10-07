using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpareParts.Data.Models;
using SpareParts.Domain.Dtos.ProductDtos;
using SpareParts.InfraStructure.Interfaces;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDomainRepository<Product> _repo;
    private readonly IMapper _mapper;

    public ProductsController(IDomainRepository<Product> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    //GET api/Products
    [HttpGet]
    public ActionResult<IEnumerable<ProductReadDto>> GetAllProduct()
    {
        var modelItems = _repo.GetAllModel();

        return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(modelItems));
    }

    //GET api/Products/{id}
    [HttpGet("{id}")]
    public ActionResult<ProductReadDto> GetProductById(int id)
    {
        var modelItem = _repo.GetById(id);
        return Ok(_mapper.Map<ProductReadDto>(modelItem));
    }

    //POST api/Products/
    [HttpPost]
    public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto createDto)
    {
        var model = _mapper.Map<Product>(createDto);
        _repo.CreateModel(model);
        _repo.SaveChanges();

        return Ok(_mapper.Map<ProductReadDto>(model));
    }

    //PUT api/Products/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateProduct(int id, ProductUpdateDto updateDto)
    {
        var modelFromRepo = _repo.GetById(id);
        _mapper.Map(updateDto, modelFromRepo);

        _repo.UpdateModel(modelFromRepo);

        _repo.SaveChanges();

        return NoContent();
    }
    
    //DELETE api/Products/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var modelFromRepo = _repo.GetById(id);
        _repo.DeleteModel(modelFromRepo);
        _repo.SaveChanges();

        return NoContent();
    }
    
}