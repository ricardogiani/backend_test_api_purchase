using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            CreateMap<Order, CreateOrderResult>();
            CreateMap<OrderItemCommand, OrderItem>();

            CreateMap<Order, GetOrderResult>();
            CreateMap<OrderItem, OrderItemResult>();
           
        }

        
    }
}