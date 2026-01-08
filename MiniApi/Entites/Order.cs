using MiniApi.Enums;

namespace MiniApi.Entites
{
    public sealed class Order 
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTimeOffset CreatedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow,TimeZoneInfo.Local);
    }
}
