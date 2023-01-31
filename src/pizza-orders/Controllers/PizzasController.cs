using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_orders.data;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzasController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetAllPizzas()
        {
            var pizzas = await _pizzaRepository.GetAllAsync();
            return Ok(pizzas);
        }



    }
}
