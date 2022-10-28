
using System.Security.Claims;

namespace SpareParts.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UsersController(
        UserManager<User> userManager,
        IAuthService authService,
        IMapper mapper)
    {
        _userManager = userManager;
        _authService = authService;
        _mapper = mapper;
    }

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

    [HttpPost("login")] 
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }
    
    [HttpGet]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var modelItems = await _userManager.Users.ToListAsync();
        IEnumerable<UserDto> result = _mapper.Map<IEnumerable<UserDto>>(modelItems);
        foreach (var userDto in result.Select((value, i) => new { i, value }))
        {
           userDto.value.Role = (await _userManager.GetClaimsAsync(modelItems[userDto.i])).FirstOrDefault(c=>c.Type==ClaimTypes.Role)!.Value;
        }
        return Ok(result);
    }

    [HttpGet("id")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var modelItem = await _userManager.FindByIdAsync(id.ToString());
        UserDto result = _mapper.Map<UserDto>(modelItem);
        result.Role = (await _userManager.GetClaimsAsync(modelItem)).FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Id not matched");
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.UpdateUserAsync(dto);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ChangePasswordAsync(dto);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }
    
    [HttpPost("forgetPassword")]
    public IActionResult ForgetPasswordAsync()
    {
        return Ok(new{Info="لسه متهندلتش "});
    }

    [HttpDelete]
    [Authorize(Policy = "Manager")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            return BadRequest("Not Deleted");

        return Ok();
    }

    #region Roles

    /*[HttpPost("manageRoles")]
    public async Task<IActionResult> ManageRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(GetRoles(userId));
    }
    private UserRolesDto GetRoles(string userId)
    {
        var user =  _userManager.FindByIdAsync(userId).Result;

        var roles =  _roleManager.Roles.ToList();

        return new UserRolesDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            Roles = roles.Select(role => new RoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
            }).ToList()
        };
    }*/

    #endregion

}