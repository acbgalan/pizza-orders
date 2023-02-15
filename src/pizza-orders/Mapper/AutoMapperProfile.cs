using AutoMapper;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Client;
using pizza_orders.Requests.Ingredient;
using pizza_orders.Requests.Pizza;
using pizza_orders.Responses.Client;
using pizza_orders.Responses.Ingredient;
using pizza_orders.Responses.Orders;
using pizza_orders.Responses.Pizza;

namespace pizza_orders.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            PizzaMapping();
            IngredientMapping();
            ClientMapping();
            OrderMapping();
        }

        private void OrderMapping()
        {
            CreateMap<Order, OrderResponse>();
        }

        public void PizzaMapping()
        {
            CreateMap<Pizza, PizzaResponse>()
                .ForMember(pizzaResponse => pizzaResponse.Ingredients, options => options.MapFrom(MapPizzaResponse));

            CreateMap<CreatePizzaRequest, Pizza>();
            //.ForMember(pizza => pizza.Ingredients, options => options.MapFrom(MapPizzaIngredients));
            //TODO: Si la relación de Pizza e ingredientes fuese con tabla intermedia manual podria funcionar el.. video 57 udemy

            CreateMap<UpdatePizzaRequest, Pizza>();
        }

        public void IngredientMapping()
        {
            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<CreateIngredientRequest, Ingredient>();
            CreateMap<UpdateIngredientRequest, Ingredient>();

            CreateMap<int, Ingredient>();
        }

        private void ClientMapping()
        {
            CreateMap<Client, ClientResponse>();
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>();
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

        //No se esta utilizando.. ver notas CreateMap<CreatePizzaRequest, Pizza>();
        private List<Ingredient> MapPizzaIngredients(CreatePizzaRequest createPizzaRequest, Pizza pizza)
        {
            var result = new List<Ingredient>();

            if (createPizzaRequest.IngredientsIds == null)
            {
                return result;
            }

            foreach (var item in createPizzaRequest.IngredientsIds)
            {
                //result.Add(_ingredientRepository.Get(item));
                result.Add(new Ingredient() { Id = item });
            }

            return result;
        }

    }
}
