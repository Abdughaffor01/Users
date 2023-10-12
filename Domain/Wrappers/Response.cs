using System.Net;
namespace Domain;
public class Response<T>
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    public Response(HttpStatusCode code) => StatusCode = (int)code;
    public Response(List<string> errors)=>Errors = errors;
    public Response(T data)
    {
        StatusCode= 200;
        Data = data;
    }
    public Response(HttpStatusCode code,string message)
    {
        StatusCode=(int)code;
        Errors= new List<string>() { message };
    }
}
