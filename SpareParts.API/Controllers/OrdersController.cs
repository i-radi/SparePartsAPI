using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpareParts.Data.Models;
using SpareParts.Domain.Dtos.OrderDtos;
using SpareParts.InfraStructure.Interfaces;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IDomainRepository<Order> _repo;
    private readonly IMapper _mapper;

    public OrdersController(IDomainRepository<Order> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    //GET api/Orders
    [HttpGet]
    public ActionResult<IEnumerable<OrderReadDto>> GetAllOrder()
    {
        var modelItems = _repo.GetAllModel();

        return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(modelItems));
    }

    //GET api/Orders/{id}
    [HttpGet("{id}")]
    public ActionResult<OrderReadDto> GetOrderById(int id)
    {
        var modelItem = _repo.GetById(id);
        return Ok(_mapper.Map<OrderReadDto>(modelItem));
    }

    //POST api/Orders/
    [HttpPost]
    public ActionResult<OrderReadDto> CreateOrder(OrderCreateDto createDto)
    {
        var model = _mapper.Map<Order>(createDto);
        _repo.CreateModel(model);
        _repo.SaveChanges();

        return Ok(_mapper.Map<OrderReadDto>(model));
    }

    //PUT api/Orders/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateOrder(int id, OrderUpdateDto updateDto)
    {
        var modelFromRepo = _repo.GetById(id);
        _mapper.Map(updateDto, modelFromRepo);

        _repo.UpdateModel(modelFromRepo);

        _repo.SaveChanges();

        return NoContent();
    }

    //DELETE api/Orders/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteOrder(int id)
    {
        var modelFromRepo = _repo.GetById(id);
        _repo.DeleteModel(modelFromRepo);
        _repo.SaveChanges();

        return NoContent();
    }

}