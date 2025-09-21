using BusinessObjects;
using BusinessObjects.Model;
using DataAccessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LaptopService : ILaptopService
    {
        private readonly LaptopRepository laptopRepository;

        public LaptopService(FptshopDbContext context)
        {
            laptopRepository = new LaptopRepository(context);
        }
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
            return await laptopRepository.GetLaptopById(id);
        }

        public async Task<List<Laptop>> GetLaptops()
        {
            return await laptopRepository.GetLaptops();
        }

        public async Task UpdateLaptop(Laptop laptop)
        {
            await laptopRepository.UpdateLaptop(laptop);
        }
    }
}
