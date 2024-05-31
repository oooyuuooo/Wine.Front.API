using Microsoft.AspNetCore.Mvc;
using Wine.BLL.Service.Orders;
using Wine.DAL.EFRepository.Orders;
using Wine.IDAL.IRepository.Orders;
using Wine.Infa.Dto.Orders;
using Wine.Infa.EFModel.Models;

namespace Wine.Front.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;
        private readonly OrderService _service;

        public OrdersController(AppDbContext context)
        {
            _repo = new OrderEFRepository(context);
            _service = new OrderService(_repo, context);
        }

        [HttpGet("GetOrder")]
        public List<OrderItemDto> GetOrder(int memberId)
        {
            return _service.GetOrder(memberId);
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrderItem(int orderId, int productId, int count)
        {
            var result = _service.UpdateCartItem(orderId, productId, count);

            return Ok(result);
        }

        [HttpDelete("DeleteOrderItem")]
        public IActionResult DeleteOrderItem(int productId)
        {
            var result = _service.DeleteOrderItem(productId);

            return Ok(result);
        }
    }
}
