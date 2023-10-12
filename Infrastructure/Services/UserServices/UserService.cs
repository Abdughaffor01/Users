using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure;
public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _file;
    public UserService(DataContext context,IMapper mapper,IFileService file)
    {
        _file = file;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GetUserDto>> AddUserAsync(AddUserDto model)
    {
        try
        {
            string fileName=string.Empty;
            if (model.Photo != null) fileName = await  _file.AddFileAsync(model.Photo!,FolderName.Images);
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName=model.LastName,
                Email = model.Email,
                City = model.City,
                Phone=model.Phone,
                Status = model.Status,
                PhotoName=fileName
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var mapped=_mapper.Map<GetUserDto>(user);
            return new Response<GetUserDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return new Response<string>(HttpStatusCode.NotFound);
            if (user.PhotoName != "") await _file.DeleteFileAsync(user.PhotoName, FolderName.Images);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK,"Successfuly deleted user");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<GetUserDto>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return new Response<GetUserDto>(HttpStatusCode.NotFound);
            var mapped = _mapper.Map<GetUserDto>(user);
            return new Response<GetUserDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<PaginationResponse<List<GetUserDto>>> GetUsersAsync(UserFilter filter)
    {
        var user = _context.Users.AsQueryable();
        if (filter.City != null) user = user.Where(u => u.City.Contains(filter.City));
        if (filter.Search != null) user = user.Where(s => s.City.Contains(filter.Search) || s.Email.Contains(filter.Search) || s.Phone.Contains(filter.Search));
        if (filter.Status != null) user = user.Where(u => u.Status==filter.Status);
        var filtered = new UserFilter(filter.PageNumber, filter.PageSize);
        var paged = await user.Skip((filtered.PageNumber - 1) * filtered.PageSize).Take(filtered.PageSize).ToListAsync();
        var totalRecords = await user.CountAsync();
        var mappedUser = _mapper.Map<List<GetUserDto>>(paged);

        return new PaginationResponse<List<GetUserDto>>(mappedUser, filtered.PageNumber, filtered.PageSize, totalRecords);
    }

    public async Task<Response<GetUserDto>> UpdateUserAsync(AddUserDto model)
    {
        try
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null) return new Response<GetUserDto>(HttpStatusCode.NotFound);
            if (model.Photo != null)
            {
                if (user.PhotoName != "") await _file.DeleteFileAsync(user.PhotoName,FolderName.Images);
                user.PhotoName = await _file.AddFileAsync(model.Photo, FolderName.Images);
            }
            user.FirstName=model.FirstName;
            user.LastName=model.LastName;
            user.City=model.City;
            user.Email=model.Email;
            user.Phone=model.Phone;
            user.Status=model.Status;
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetUserDto>(user);
            return new Response<GetUserDto>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }
}
