using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpareParts.API.Services.Auth;
using SpareParts.Data.Models;
using SpareParts.Domain.Dtos.IdentityDtos;
using SpareParts.Domain.Dtos.Jwt;
using SpareParts.Domain.Dtos.UserDtos;
using SpareParts.Domain.Repos;
using SpareParts.InfraStructure.Enums;
using SpareParts.InfraStructure.Interfaces;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IAuthService _authService;

    private readonly UserRepo _repo;

    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager,
        IAuthService authService,
        UserRepo repo,
        IMapper mapper)
    {
        _userManager = userManager;
        _authService = authService;
        _repo = repo;
        _mapper = mapper;
    }

    

    //[Authorize(Roles = "Advisor")]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(dto);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("login")]//token  
    public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.GetTokenAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    //GET api/Users
    [HttpGet]
    public ActionResult<IEnumerable<UserReadDto>> GetAll(UserType type = UserType.None)
    {
        IEnumerable<User> modelItems = null;
        if (type == UserType.None)
            modelItems = _repo.GetAllModel();
        else
            modelItems = _repo.GetAllModel().Where(u => u.UserType == type);

        return Ok(_mapper.Map<IEnumerable<UserReadDto>>(modelItems));
    }

    //GET api/Users/{id}
    [HttpGet("id")]
    public ActionResult<UserReadDto> GetSimpleUserById(Guid id)
    {
        var modelItem = _repo.GetById(id);
        return Ok(_mapper.Map<UserReadDto>(modelItem));
    }
    
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ChangePasswordAsync(dto);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    //DELETE api/Users/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(Guid id)
    {
        var modelFromRepo = _repo.GetById(id);
        _repo.DeleteModel(modelFromRepo);
        _repo.SaveChanges();

        return NoContent();
    }
}