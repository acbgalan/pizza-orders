using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_orders.data;
using pizza_orders.data.Models;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            return Ok(pizzas);
        }

    }
}
