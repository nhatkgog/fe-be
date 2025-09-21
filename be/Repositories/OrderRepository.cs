using BusinessObjects.Model;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrderRepository orderRepository;

        public static OrderRepository GetInstance()
        {
            if (orderRepository == null)
            {
                orderRepository = new OrderRepository();
            }
            return orderRepository;
        }

        public async Task CreateOrder(Order order)
        {
            await OrderDAO.SaveOrder(order);
        }
    }
}
