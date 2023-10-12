using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)=>_service = service;

    [HttpGet("GetUsersAsync")]
    public async Task<PaginationResponse<List<GetUserDto>>> GetUsersAsync(UserFilter filter) { 
        return await _service.GetUsersAsync(filter);
    }

    [HttpGet("GetUserByIdAsync")]
    public async Task<Response<GetUserDto>> GetUserByIdAsync(int id) {
        return await _service.GetUserByIdAsync(id);
    }

    [HttpPost("AddUserAsync")]
    public async Task<Response<GetUserDto>> AddUserAsync(AddUserDto model) { 
        return await _service.AddUserAsync(model);
    }

    [HttpPut("UpdateUserAsync")]
    public async Task<Response<GetUserDto>> UpdateUserAsync(AddUserDto model) {
        return await _service.UpdateUserAsync(model);
    }

    [HttpDelete("DeleteUserAsync")]
    public async Task<Response<string>> DeleteUserAsync(int id) { 
        return await _service.DeleteUserAsync(id);
    }
}
