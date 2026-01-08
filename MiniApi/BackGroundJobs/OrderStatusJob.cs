
using Microsoft.EntityFrameworkCore;
using MiniApi.Datas;
using MiniApi.Enums;

namespace MiniApi.BackGroundJobs
{
    public sealed class OrderStatusJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderStatusJob> _logger;
        public OrderStatusJob(IServiceScopeFactory scopeFactory, ILogger<OrderStatusJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateOrderStatus();
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
        private async Task UpdateOrderStatus()
        {
             using var scope= _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            DateTimeOffset correctDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow.AddSeconds(-5),TimeZoneInfo.Local);
            var orders = await dbContext.Orders.Where(o => o.Status == OrderStatus.Pending && o.CreatedDate<=correctDate).ToListAsync();
            if (!orders.Any())
            {
                _logger.LogInformation("No pending orders found.");
                return;

            }

            foreach ( var order in orders)
            {
                order.Status = OrderStatus.Completed;
            }
            await dbContext.SaveChangesAsync();
            _logger.LogInformation("{Count} orders successfully compleated at {Time}",orders.Count, TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow,TimeZoneInfo.Local));
        } 
    }
}
