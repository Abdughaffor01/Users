namespace Domain;   
public class UserFilter:PaginationFilter
{
    public Status? Status { get; set; }
    public string? City { get; set; }
    public string? Search { get; set; }
    public UserFilter() {}
    public UserFilter(int pageNumber,int pageSize):base(pageNumber,pageSize){}
}
