using MiniApi.Dtos.Orders;
using MiniApi.ResponseModels;

namespace MiniApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddAsync(OrderCreateDto dto);
        Task<OrderDetailDto> GetByIdAsync(int id,bool hasTracking=false);
        Task<PaginatedResult<OrderDto>> GetOrdersWithPaginatedAsync(int page=1 , int pageSize = 10);
    }
}
