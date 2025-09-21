using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILaptopRepository
    {
        Task<List<Laptop>> GetLaptops();
        Task<Laptop> GetLaptopById(int id);
        Task CreateLaptop(Laptop laptop);
        Task UpdateLaptop(Laptop laptop);
        Task DeleteLaptop(int id);
    }
}
