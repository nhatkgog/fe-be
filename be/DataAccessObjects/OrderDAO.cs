using BusinessObjects;
using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        public static async Task<Order> SaveOrder(Order order)
        {
            try
            {
                using var context = new FptshopDbContext();
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException dbEx)
            {
                // this will surface the FK/NOT NULL/identity error
                var sqlError = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new InvalidOperationException(
                    $"Could not save Order. Database said: {sqlError}",
                    dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
