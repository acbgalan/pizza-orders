﻿using AutoMapper;
using pizza_orders.data.Models;
using pizza_orders.Requests.Pizza;
using pizza_orders.Responses.Pizza;

namespace pizza_orders.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            PizzaMapping();
        }

        public void PizzaMapping()
        {
            CreateMap<Pizza, PizzaResponse>();
            CreateMap<CreatePizzaRequest, Pizza>();
        }

    }
}