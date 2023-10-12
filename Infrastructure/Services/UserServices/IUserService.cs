using Domain;
namespace Infrastructure;
public interface IUserService
{
    Task<PaginationResponse<List<GetUserDto>>> GetUsersAsync(UserFilter filter);
    Task<Response<GetUserDto>> GetUserByIdAsync(int id);
    Task<Response<GetUserDto>> AddUserAsync(AddUserDto model);
    Task<Response<GetUserDto>> UpdateUserAsync(AddUserDto model);
    Task<Response<string>> DeleteUserAsync(int id);
}
