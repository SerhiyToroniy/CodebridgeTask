using CodebridgeTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs([FromQuery] string attribute, [FromQuery] string order, [FromQuery] int? pageNumber, [FromQuery] int? limit)
        {
            IQueryable<Dog> dogsQuery = _context.Dogs;

            if (!string.IsNullOrEmpty(attribute))
            {
                if (attribute.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        dogsQuery = dogsQuery.OrderByDescending(d => d.Name);
                    else
                        dogsQuery = dogsQuery.OrderBy(d => d.Name);
                }
                else if (attribute.Equals("color", StringComparison.OrdinalIgnoreCase))
                {
                    if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                        dogsQuery = dogsQuery.OrderByDescending(d => d.Color);
                    else
                        dogsQuery = dogsQuery.OrderBy(d => d.Color);
                }
                // Add more conditions for other attributes if needed
            }

            if (pageNumber.HasValue && limit.HasValue)
            {
                dogsQuery = dogsQuery.Skip((pageNumber.Value - 1) * limit.Value).Take(limit.Value);
            }

            var dogs = await dogsQuery.ToListAsync();
            return dogs;
        }

        [HttpPost]
        public async Task<ActionResult<Dog>> CreateDog(Dog dog)
        {
            if (string.IsNullOrEmpty(dog.Name) || dog.TailLength < 0 || dog.Weight < 0)
            {
                return BadRequest("Invalid dog data.");
            }

            if (_context.Dogs.AsEnumerable().Any(d => d.Name.Equals(dog.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("A dog with the same name already exists.");
            }

            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDogs), new { id = dog.Id }, dog);
        }
    }

}
