namespace pagination.Queries;

public class ItemQuery
{
    public string SearchPhrase { get; set; } = default!;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
