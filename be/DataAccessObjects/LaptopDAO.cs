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
    public class LaptopDAO
    {
        private readonly FptshopDbContext _context;
        public LaptopDAO(FptshopDbContext context)
        {
            _context = context;
        }
        public async Task<List<Laptop>> GetAllLaptopsAsync()
        {
            var listLaptops = new List<Laptop>();
            try
            {
                //using var db = new FptshopDbContext();
                listLaptops = await _context.Laptops.Include(f => f.Orders).Include(f => f.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listLaptops;
        }
        public async Task<Laptop> CreateLaptop(Laptop laptop)
        {
            try
            {
                //using var context = new FptshopDbContext();
                await _context.Laptops.AddAsync(laptop);
                await _context.SaveChangesAsync();
                return laptop;
            }
            catch (DbUpdateException dbEx)
            {
                // this will surface the FK/NOT NULL/identity error
                var sqlError = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new InvalidOperationException(
                    $"Could not save Laptop. Database said: {sqlError}",
                    dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateLaptop(Laptop laptop)
        {
            try
            {
                //using var context = new FptshopDbContext();
                _context.Entry<Laptop>(laptop).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteLaptop(int id)
        {
            try
            {
                //using var context = new FptshopDbContext();
                var l1 = await _context.Laptops.SingleOrDefaultAsync(c => c.LaptopId == id);
                _context.Laptops.Remove(l1);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Laptop> GetLaptopByIdAsync(int id)
        {
            //using var db = new FptshopDbContext();
            return await _context.Laptops.Include(f => f.Orders).Include(f => f.User).FirstOrDefaultAsync(c => c.LaptopId.Equals(id));
        }
    }
}
