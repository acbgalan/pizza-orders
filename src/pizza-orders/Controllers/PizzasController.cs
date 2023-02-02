using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_orders.data;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Pizza;
using pizza_orders.Responses.Pizza;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;

        public PizzasController(IPizzaRepository pizzaRepository, IMapper mapper)
        {
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PizzaResponse>>> GetAllPizzas()
        {
            var pizzas = await _pizzaRepository.GetAllAsync();
            var pizzasResponses = _mapper.Map<List<PizzaResponse>>(pizzas);

            return Ok(pizzas);
        }

        [HttpGet("id:int", Name = "GetPizza")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PizzaResponse>> GetPizza(int id)
        {
            var pizza = await _pizzaRepository.GetAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            var pizzaResponse = _mapper.Map<PizzaResponse>(pizza);
            return Ok(pizzaResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePizza([FromBody] CreatePizzaRequest createPizzaRequest)
        {
            if(createPizzaRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pizza = _mapper.Map<Pizza>(createPizzaRequest);
            await _pizzaRepository.AddAsync(pizza);
            int saveResult = await _pizzaRepository.SaveAsync();

            if(!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al guardar item");
            }

            return CreatedAtRoute("GetPizza", new { id = pizza.Id }, pizza);
        }

    }
}
