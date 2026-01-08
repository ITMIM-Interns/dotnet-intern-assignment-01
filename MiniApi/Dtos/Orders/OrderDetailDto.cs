namespace MiniApi.Dtos.Orders
{
    public sealed record OrderDetailDto
        (
            int id, 
            string CustomerName,
            string ProductName,
            string CreatedDate,
            int Quantity,
            decimal Price
        );
}
   
