namespace MiniApi.ResponseModels
{
    public sealed record PaginatedResult<T>
    (
        IReadOnlyList<T> Items,
        int TotalPage
       
    );
   
}
