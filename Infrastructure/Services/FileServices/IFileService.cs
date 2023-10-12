using Microsoft.AspNetCore.Http;
namespace Infrastructure;
public interface IFileService
{
    Task<string> AddFileAsync(IFormFile file,string folderName);
    Task<bool> DeleteFileAsync(string fileName,string folderName);
}
