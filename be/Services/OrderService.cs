using BusinessObjects.Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        public async Task CreateOrder(Order order)
        {
            await OrderRepository.GetInstance().CreateOrder(order);
        }
    }
}
