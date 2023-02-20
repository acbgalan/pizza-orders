using AutoMapper;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using pizza_orders.Requests.Client;
using pizza_orders.Requests.Ingredient;
using pizza_orders.Requests.Orders;
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

        #region Mappings
        public void PizzaMapping()
        {
            CreateMap<Pizza, PizzaResponse>()
                .ForMember(d => d.Ingredients, o => o.MapFrom(MapPizzaResponse));

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

        private void OrderMapping()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Client.Name))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Client.Address))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Client.Phone))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Client.Email))
                .ForMember(d => d.Details, o => o.MapFrom(MapOrderResponseDetails));

            CreateMap<CreateOrderRequest, Order>()
                .ForMember(d => d.OrderDetails, o => o.MapFrom(Order_OrderDetails));
        }

        #endregion


        #region MapMethods
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

        private List<OrderDetail> Order_OrderDetails(CreateOrderRequest createOrderRequest, Order order)
        {
            var result = new List<OrderDetail>();

            if (createOrderRequest.Details == null)
            {
                return result;
            }

            foreach (var item in createOrderRequest.Details)
            {
                result.Add(new OrderDetail() { PizzaId = item.PizzaId, Quantity = item.Quantity });
            }

            return result;
        }

        public List<OrderResponseDetail> MapOrderResponseDetails(Order order, OrderResponse orderResponse)
        {
            var result = new List<OrderResponseDetail>();

            if (order.OrderDetails == null)
            {
                return result;
            }

            foreach (var item in order.OrderDetails)
            {
                result.Add(new OrderResponseDetail()
                {
                    PizzaId = item.PizzaId,
                    Product = item.Pizza.Name,
                    Quantity = item.Quantity,
                    UnitPrize = item.UnitPrize,
                    Discount = item.Discount,
                    Amount = item.Amount
                });
            }

            return result;
        }

        #endregion

    }
}
