using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace Infrastructure;
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    public FileService(IWebHostEnvironment environment)=>_environment=environment;
    public async Task<string> AddFileAsync(IFormFile file, string folderName)
    {
        try
        {
            string folderPath = Path.Combine(_environment.WebRootPath, folderName);
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            if (Directory.Exists(folderPath) == false) Directory.CreateDirectory(folderPath);
            folderPath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(folderPath, FileMode.OpenOrCreate);
            await file.CopyToAsync(stream);
            return fileName;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public async Task<bool> DeleteFileAsync(string fileName, string folderName)
    {
        try
        {
            return await Task.Run(() =>
            {
                string fullPath = Path.Combine(_environment.WebRootPath, folderName, fileName);
                File.Delete(fullPath);
                return true;
            });
        }
        catch (Exception)
        {
            return false;
        }
    }
}
