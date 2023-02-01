﻿using AutoMapper;
using pizza_orders.data.Models;
using pizza_orders.Responses.Pizza;

namespace pizza_orders.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pizza, PizzaResponse>();
        }
    }
}
