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
        public async Task<IngredientResponse> GetIngredient(int id)
        {
            var ingredient = await _ingredientRepository.GetAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientResponse = _mapper.Map<IngredientResponse>(ingredient);
            return Ok(ingredientResponse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIngredient([FromBody] CreateIngredientRequest createIngredientRequest)
        {
            if (createIngredientRequest == null)
            {
                return BadRequest();
            }

            var ingredient = _mapper.Map<Ingredient>(createIngredientRequest);
            await _ingredientRepository.AddAsync(ingredient);
            int saveResult = _ingredientRepository.SaveAsync();

            if ((saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);





        }


    }
}
