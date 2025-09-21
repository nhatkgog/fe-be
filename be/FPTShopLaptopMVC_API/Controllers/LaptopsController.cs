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
using DataAccessObjects;

namespace FPTShopLaptopMVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        private readonly ILaptopService laptopService;

        public LaptopsController(FptshopDbContext context)
        {
            laptopService = new LaptopService(context);
        }

        // GET: api/Laptops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops()
        {
            return await laptopService.GetLaptops();
        }

        // GET: api/Laptops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laptop>> GetLaptop(int id)
        {
            var laptop = await laptopService.GetLaptopById(id);

            if (laptop == null)
            {
                return NotFound();
            }

            return laptop;
        }

        // PUT: api/Laptops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaptop(int id, [FromBody] LaptopDTO laptopDTO)
        {
            if (laptopDTO == null)
                return BadRequest("Laptop payload is required.");

            var existing = await laptopService.GetLaptopById(id);
            if (existing == null)
                return NotFound();

            if (id != existing.LaptopId)
            {
                return BadRequest();
            }

            existing.Name = laptopDTO.Name;
            existing.Brand = laptopDTO.Brand;
            existing.CreatedAt = laptopDTO.CreatedAt;
            existing.Price = laptopDTO.Price;
            existing.StockQuantity = laptopDTO.StockQuantity;
            existing.UserId = laptopDTO.UserId;

            await laptopService.UpdateLaptop(existing);

            return NoContent();
        }

        // POST: api/Laptops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laptop>> PostLaptop([FromBody] LaptopDTO laptopDTO)
        {
            Laptop laptop = new Laptop
            {
                Name = laptopDTO.Name,
                Brand = laptopDTO.Brand,
                CreatedAt = laptopDTO.CreatedAt,
                Price = laptopDTO.Price,
                StockQuantity = laptopDTO.StockQuantity,
                UserId = laptopDTO.UserId,
            };
            await laptopService.CreateLaptop(laptop);

            return CreatedAtAction("GetLaptop", new { id = laptop.LaptopId }, laptop);
        }

        // DELETE: api/Laptops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaptop(int id)
        {
            var laptop = await laptopService.GetLaptopById(id);
            if (laptop == null)
            {
                return NotFound();
            }

            await laptopService.DeleteLaptop(id);

            return NoContent();
        }
    }
}
