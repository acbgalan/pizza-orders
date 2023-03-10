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
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public PizzasController(IPizzaRepository pizzaRepository, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _pizzaRepository = pizzaRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PizzaResponse>>> GetAllPizzas()
        {
            var pizzas = await _pizzaRepository.GetAllAsync();
            var pizzasResponses = _mapper.Map<List<PizzaResponse>>(pizzas);

            return Ok(pizzasResponses);
        }

        [HttpGet("id:int", Name = "GetPizza")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PizzaResponse>> GetPizza(int id)
        {
            var pizza = await _pizzaRepository.GetAsync(id);

            if (pizza == null)
            {
                return NotFound("Pizza no encontrada");
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
            if (createPizzaRequest == null)
            {
                return BadRequest();
            }

            if (await _pizzaRepository.ExitsAsync(createPizzaRequest.Name))
            {
                return BadRequest("Ya existe una pizza con ese nombre");
            }

            bool ingredientsExits = await _ingredientRepository.ExitsAsync(createPizzaRequest.IngredientsIds);

            if (!ingredientsExits)
            {
                return BadRequest("No existe uno/s de los ingredientes");
            }

            var pizza = _mapper.Map<Pizza>(createPizzaRequest);
            pizza.Ingredients = await _ingredientRepository.GetAsync(createPizzaRequest.IngredientsIds);
            await _pizzaRepository.AddAsync(pizza);
            int saveResult = await _pizzaRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al guardar nueva pizza");
            }

            var pizzaResponse = _mapper.Map<PizzaResponse>(pizza);

            return CreatedAtRoute("GetPizza", new { id = pizza.Id }, pizzaResponse);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePizza(UpdatePizzaRequest updatePizzaRequest)
        {
            if (updatePizzaRequest == null)
            {
                return BadRequest();
            }

            var exits = await _pizzaRepository.ExitsAsync(updatePizzaRequest.Id);

            if (!exits)
            {
                return NotFound("Pizza no encontrada");
            }

            var pizza = _mapper.Map<Pizza>(updatePizzaRequest);
            await _pizzaRepository.UpdateAsync(pizza);
            int saveResult = await _pizzaRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al guardar pizza");
            }

            return NoContent();
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePizza(int id)
        {
            var exits = await _pizzaRepository.ExitsAsync(id);

            if (!exits)
            {
                return NotFound("Pizza no encontrada");
            }

            await _pizzaRepository.DeleteAsync(id);
            int saveResult = await _pizzaRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al borrar pizza");
            }

            return NoContent();
        }

    }
}
