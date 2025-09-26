using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateOrderHandler
        /// </summary>
        /// <param name="orderRepository">The Order repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                throw new NotFoundException($"Order id {request.Id} not Found", null);
            
            order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.Status);

            order = await _orderRepository.UpdateAsync(order, cancellationToken);

            return _mapper.Map<UpdateOrderResult>(order);
        }
    }
}