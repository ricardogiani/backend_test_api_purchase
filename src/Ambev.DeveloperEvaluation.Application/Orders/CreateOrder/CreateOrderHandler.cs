using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;        
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IUserRepository _userRepository;
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
            IBranchRepository branchRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _branchRepository = branchRepository;
            _userRepository = userRepository;

            _mapper = mapper;
        }

        public async Task ValidateItem(OrderItemCommand itemCommand)
        {
            var product = await _productRepository.GetByIdAsync(itemCommand.ProductId);
            if (product == null)
                throw new NotFoundException($"Product id {itemCommand.ProductId} not found", null);
        }

        public async Task ValidateDataRelations(CreateOrderCommand request)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
                throw new NotFoundException($"Customer id {request.CustomerId} not found", null);

            var user = _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException($"User id {request.CustomerId} not found", null);

            var branch = _branchRepository.GetByIdAsync(request.UserId);
            if (branch == null)
                throw new NotFoundException($"Branch id {request.CustomerId} not found", null);
        }
                

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var validator = new CreateOrderCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                await ValidateDataRelations(request);

                Order newOrder = _mapper.Map<Order>(request);

                foreach (var orderItemCmd in request.OrderItems)
                {
                    await ValidateItem(orderItemCmd);
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