using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrderItem
{
    public class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItemCommand, DeleteOrderItemResult>
    {        
        public readonly IOrderRepository _orderRepository;

        public readonly IMapper _mapper;

        private readonly ILogger<DeleteOrderItemHandler> _logger;

        public DeleteOrderItemHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderItemHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeleteOrderItemResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Receive request: {request}");

            try
            {
                var order = await _orderRepository.GetByIdWithIncludesAsync(request.OrderId, cancellationToken, x => x.OrderItems );
                if (order == null)
                    throw new NotFoundException($"Order id {request.OrderId} not Found", null);

                var orderItem = order.OrderItems.FirstOrDefault(i => i.ProductId == request.ProductId);
                if (orderItem == null)
                    throw new NotFoundException($"Item for Product id {request.ProductId} not Found", null);

                bool success = order.RemoveItem(orderItem);

                await _orderRepository.UpdateAsync(order);

                return new DeleteOrderItemResult { Success = success };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}