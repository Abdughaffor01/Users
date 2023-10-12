using Microsoft.AspNetCore.Http;
namespace Domain;
public class AddUserDto:BaseUserDto
{
    public IFormFile? Photo { get; set; }
    public Status Status { get; set; }
}
