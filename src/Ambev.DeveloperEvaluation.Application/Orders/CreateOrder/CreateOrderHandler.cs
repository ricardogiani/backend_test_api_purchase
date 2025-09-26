using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateOrderHandler
        /// </summary>
        /// <param name="orderRepository">The Order repository</param>
        /// <param name="productRepository">The Product repository</param>
        /// <param name="customerRepository">The Customer repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CreateOrderHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // TODO Validate and Logs

            try
            {
                Order newOrder = _mapper.Map<Order>(request);

                foreach (var orderItemCmd in request.OrderItems)
                {
                    OrderItem orderItem = _mapper.Map<OrderItem>(orderItemCmd);

                    newOrder.AddItem(orderItem);
                }

                var orderResult = await _orderRepository.CreateAsync(newOrder, cancellationToken);

                return _mapper.Map<CreateOrderResult>(orderResult);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}