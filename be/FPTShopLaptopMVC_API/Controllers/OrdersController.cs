using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using BusinessObjects.Model;
using Services;
using BusinessObjects.DTO;

namespace FPTShopLaptopMVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController()
        {
            orderService = new OrderService();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] OrderDTO orderDTO)
        {
            Order order = new Order
            {
                LaptopId = orderDTO.LaptopId,
                OrderDate = orderDTO.OrderDate,
                Quantity = orderDTO.Quantity,
                UserId = orderDTO.UserId,
            };
            await orderService.CreateOrder(order);

            return NoContent();
        }

    }
}
