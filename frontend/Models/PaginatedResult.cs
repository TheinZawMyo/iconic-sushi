using System.Collections.Generic;
using System.Linq;
public class PaginatedResult<T>
{
    public List<T>? Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPreviousPage
    {
        get { return PageNumber > 1; }
    }

    public bool HasNextPage
    {
        get { return PageNumber < TotalPages; }
    }

    public int TotalPages
    {
        get { return (int)System.Math.Ceiling(TotalCount / (double)PageSize); }
    }

    public IEnumerable<int> GetPageNumbers()
    {
        for (int i = 1; i <= TotalPages; i++)
        {
            yield return i;
        }
    }

}