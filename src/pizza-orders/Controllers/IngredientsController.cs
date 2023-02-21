using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Ingredient;
using pizza_orders.Responses.Ingredient;

namespace pizza_orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<IngredientResponse>>> GetAllIngredients()
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            var ingredientsResponse = _mapper.Map<List<IngredientResponse>>(ingredients);

            return Ok(ingredientsResponse);

        }

        [HttpGet("id:int", Name = "GetIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IngredientResponse>> GetIngredient(int id)
        {
            var ingredient = await _ingredientRepository.GetAsync(id);

            if (ingredient == null)
            {
                return NotFound("Ingrediente no encontrado");
            }

            var ingredientResponse = _mapper.Map<IngredientResponse>(ingredient);
            return Ok(ingredientResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateIngredient([FromBody] CreateIngredientRequest createIngredientRequest)
        {
            if (createIngredientRequest == null)
            {
                return BadRequest();
            }

            var ingredient = _mapper.Map<Ingredient>(createIngredientRequest);


            await _ingredientRepository.AddAsync(ingredient);
            int saveResult = await _ingredientRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateIngredient(UpdateIngredientRequest updateIngredientRequest)
        {
            if (updateIngredientRequest == null)
            {
                return BadRequest();
            }

            var exits = await _ingredientRepository.ExitsAsync(updateIngredientRequest.Id);

            if (!exits)
            {
                return NotFound("Ingrediente no encontrado");
            }

            var ingredient = _mapper.Map<Ingredient>(updateIngredientRequest);
            await _ingredientRepository.UpdateAsync(ingredient);
            int saveResult = await _ingredientRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Valor no esperado al actualizar ingrediente");
            }

            return NoContent();
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteIngredient(int id)
        {
            var exits = await _ingredientRepository.ExitsAsync(id);

            if (!exits)
            {
                return NotFound("Ingrediente no encontrado");
            }

            await _ingredientRepository.DeleteAsync(id);
            int saveResult = await _ingredientRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Valor no esperado al borrar ingrediente {id}");
            }

            return NoContent();
        }

    }
}
