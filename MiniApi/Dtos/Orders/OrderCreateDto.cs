namespace MiniApi.Dtos.Orders
{
    public sealed record OrderCreateDto(
        string CustomerName,
        string ProductName,
        decimal CurrentPrice,
        int Quantity
        );
   
}
