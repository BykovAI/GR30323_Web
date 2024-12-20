using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GR30323_Web.API.Data;
using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;

namespace GR30323_Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Car>>>> GetCars(
            [FromQuery] string? categoryNormalizedName,
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 3)
        {
            var response = new ResponseData<ListModel<Car>>();

            // Формируем базовый запрос
            var query = _context.Cars.Include(c => c.Category).AsQueryable();

            // Фильтрация по категории (если указана)
            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                query = query.Where(c => c.Category.NormalizedName == categoryNormalizedName);
            }

            // Общий подсчёт записей для пагинации
            int totalItems = await query.CountAsync();

            // Пагинация
            var cars = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Формируем результат
            var result = new ListModel<Car>
            {
                Items = cars,
                CurrentPage = pageNo,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };

            response.Data = result;
            response.Success = cars.Any();
            response.ErrorMessage = cars.Any() ? null : "Нет автомобилей для заданных условий.";

            return Ok(response);
        }


        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
