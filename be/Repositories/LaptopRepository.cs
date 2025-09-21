using BusinessObjects;
using BusinessObjects.Model;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly LaptopDAO laptopRepository;

        public LaptopRepository(FptshopDbContext context)
        {
            laptopRepository = new LaptopDAO(context);
        }

        //public static LaptopRepository GetInstance()
        //{
        //    if (laptopRepository == null)
        //    {
        //        laptopRepository = new LaptopRepository();
        //    }
        //    return laptopRepository;
        //}

        public async Task CreateLaptop(Laptop laptop)
        {
            await laptopRepository.CreateLaptop(laptop);
        }

        public async Task DeleteLaptop(int id)
        {
            await laptopRepository.DeleteLaptop(id);
        }

        public async Task<Laptop> GetLaptopById(int id)
        {
            return await laptopRepository.GetLaptopByIdAsync(id);
        }

        public async Task<List<Laptop>> GetLaptops()
        {
            return await laptopRepository.GetAllLaptopsAsync();
        }

        public async Task UpdateLaptop(Laptop laptop)
        {
            await laptopRepository.UpdateLaptop(laptop);
        }
    }
}
