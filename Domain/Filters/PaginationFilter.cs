namespace Domain;
public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter() { }
    public PaginationFilter(int pageNumber,int pageSize)
    {
        if (pageNumber <= 0) PageNumber = 1;
        else PageNumber = pageNumber;
        if (pageSize <= 0) PageSize = 10;
        else PageSize = pageSize;
    }
}
