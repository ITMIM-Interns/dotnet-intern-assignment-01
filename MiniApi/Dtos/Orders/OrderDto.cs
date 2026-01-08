using MiniApi.Enums;

namespace MiniApi.Dtos.Orders
{
    public sealed record OrderDto(
            string CustomerName,
            string ProductName,
            string CreatedDate,
            int Quantity,
            decimal Price,
            string Status
        );
    
}
