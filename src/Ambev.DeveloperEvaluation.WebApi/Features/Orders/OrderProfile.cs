using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Orders;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateOrderResult, CreateOrderResponse>();
            CreateMap<OrderItemRequest, OrderItemCommand>();
            CreateMap<GetOrderResult, GetOrderResponse>();
            
        }

        
    }
}