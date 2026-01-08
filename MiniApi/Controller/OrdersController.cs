using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniApi.Datas;
using MiniApi.Dtos.Orders;
using MiniApi.ResponseModels;
using MiniApi.Services.Interfaces;

namespace MiniApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
         private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add([FromBody] OrderCreateDto request)
        {
            int id = await _orderService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new {id=id},id);
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<OrderDto>>> GetOrdersWithPaginated([FromQuery]int page=1,[FromQuery]int pageSize = 10)
        {
            var response=await _orderService.GetOrdersWithPaginatedAsync(page,pageSize);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDto>> GetById([FromRoute] int id)
        {
            var response = await _orderService.GetByIdAsync(id);
            return Ok(response);
        }
    }
}
