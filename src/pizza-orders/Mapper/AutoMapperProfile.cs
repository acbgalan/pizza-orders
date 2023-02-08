using AutoMapper;
using pizza_orders.data.Models;
using pizza_orders.Requests.Ingredient;
using pizza_orders.Requests.Pizza;
using pizza_orders.Responses.Ingredient;
using pizza_orders.Responses.Pizza;

namespace pizza_orders.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            PizzaMapping();
            IngredientMapping();
        }

        public void PizzaMapping()
        {
            CreateMap<Pizza, PizzaResponse>()
                .ForMember(pizzaResponse => pizzaResponse.Ingredients, options => options.MapFrom(MapPizzaResponse));

            CreateMap<CreatePizzaRequest, Pizza>();
            CreateMap<UpdatePizzaRequest, Pizza>();
        }

        public void IngredientMapping()
        {
            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<CreateIngredientRequest, Ingredient>();
            CreateMap<UpdateIngredientRequest, Ingredient>();
        }


        private List<string> MapPizzaResponse(Pizza pizza, PizzaResponse pizzaResponse)
        {
            var result = new List<string>();

            if (pizza.Ingredients == null)
            {
                return result;
            }

            foreach (var item in pizza.Ingredients)
            {
                result.Add(item.Name);
            }

            return result;
        }

    }
}
