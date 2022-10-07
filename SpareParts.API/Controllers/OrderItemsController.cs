using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpareParts.Data.Models;
using SpareParts.Domain.Dtos.OrderItemDtos;
using SpareParts.InfraStructure.Interfaces;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IDomainRepository<OrderItem> _repo;
    private readonly IMapper _mapper;

    public OrderItemsController(IDomainRepository<OrderItem> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    //GET api/OrderItems
    [HttpGet]
    public ActionResult<IEnumerable<OrderItemReadDto>> GetAllOrderItem()
    {
        var modelItems = _repo.GetAllModel();

        return Ok(_mapper.Map<IEnumerable<OrderItemReadDto>>(modelItems));
    }

    //GET api/OrderItems/{id}
    [HttpGet("{id}")]
    public ActionResult<OrderItemReadDto> GetOrderItemById(int id)
    {
        var modelItem = _repo.GetById(id);
        return Ok(_mapper.Map<OrderItemReadDto>(modelItem));
    }

    //POST api/OrderItems/
    [HttpPost]
    public ActionResult<OrderItemReadDto> CreateOrderItem(OrderItemCreateDto createDto)
    {
        var model = _mapper.Map<OrderItem>(createDto);
        _repo.CreateModel(model);
        _repo.SaveChanges();

        return Ok(_mapper.Map<OrderItemReadDto>(model));
    }

    //PUT api/OrderItems/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateOrderItem(int id, OrderItemUpdateDto updateDto)
    {
        var modelFromRepo = _repo.GetById(id);
        _mapper.Map(updateDto, modelFromRepo);

        _repo.UpdateModel(modelFromRepo);

        _repo.SaveChanges();

        return NoContent();
    }

    //DELETE api/OrderItems/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteOrderItem(int id)
    {
        var modelFromRepo = _repo.GetById(id);
        _repo.DeleteModel(modelFromRepo);
        _repo.SaveChanges();

        return NoContent();
    }

}