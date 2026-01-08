using Microsoft.EntityFrameworkCore;
using MiniApi.Controller;
using MiniApi.Datas;
using MiniApi.Dtos.Orders;
using MiniApi.Entites;
using MiniApi.Exceptions;
using MiniApi.ResponseModels;
using MiniApi.Services.Interfaces;

namespace MiniApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrdersController> _log;

        public OrderService(AppDbContext context, ILogger<OrdersController> log)
        {
            _context = context;
            _log = log;
        }
        public async Task<int> AddAsync(OrderCreateDto dto)
        {
            Order newOrder = new Order
            {
                ProductName= dto.ProductName,
                CustomerName= dto.CustomerName,
                Quantity= dto.Quantity,
                CurrentPrice= dto.CurrentPrice,
            };
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            _log.LogInformation("New order created {Time}", TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.Local));
            return newOrder.Id;
        }

        public async Task<OrderDetailDto> GetByIdAsync(int id,bool hasTracking=false)
        {
            Order? order = new();
            if (hasTracking)
                order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            else order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
            {
                _log.LogInformation("Order not Found ID: {id}",id);
                throw new NotFoundException(ExceptionMessages.OrderNotFound);
            }
                
            return new OrderDetailDto(order.Id, order.CustomerName, order.ProductName, order.CreatedDate.ToString(), order.Quantity, order.CurrentPrice);
        }

        public async Task<PaginatedResult<OrderDto>> GetOrdersWithPaginatedAsync(int page = 1, int pageSize = 10)
        {
            int ordersCount = await _context.Orders.CountAsync();
            if (pageSize <= 0)
                pageSize = 10;
            int totalPage = (int)Math.Ceiling((double)ordersCount / pageSize);
            if (totalPage <= 0)
                throw new NotFoundException(ExceptionMessages.OrderNotFound);
            var query = _context.Orders.AsNoTracking();

            IReadOnlyList<OrderDto> items =await query.OrderBy(x => x.Id).Skip((Math.Clamp(page, 1, totalPage) - 1)*pageSize)
                .Take(pageSize).Select(o=>new OrderDto(o.CustomerName,o.ProductName,o.CreatedDate.ToString(),o.Quantity,o.CurrentPrice,o.Status.ToString())).ToListAsync();

                return new PaginatedResult<OrderDto>(items, totalPage);
            
        }
    }
}
