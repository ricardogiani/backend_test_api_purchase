using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
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

        public async Task ValidateOrderItems(IEnumerable<OrderItemCommand> orderItemsCmd, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderItemCommandValidator();

            foreach (var orderItemCmd in orderItemsCmd)
            {
                var validationResult = await validator.ValidateAsync(orderItemCmd, cancellationToken);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);                
            }
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product id {id} not found", null);

            return product;
        }

        public async Task ValidateDataRelations(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
                throw new NotFoundException($"Customer id {request.CustomerId} not found", null);

            var user = _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException($"User id {request.CustomerId} not found", null);

            var branch = _branchRepository.GetByIdAsync(request.UserId, cancellationToken);
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

                await ValidateOrderItems(request.OrderItems, cancellationToken);

                await ValidateDataRelations(request, cancellationToken);

                Order newOrder = _mapper.Map<Order>(request);

                foreach (var orderItemCmd in request.OrderItems)
                {
                    OrderItem orderItem = _mapper.Map<OrderItem>(orderItemCmd);

                    var product = await GetProduct(orderItem.ProductId);

                    orderItem.UnitPrice = product.Price;
                    newOrder.AddItem(orderItem);
                }

                newOrder.Status = OrderStatus.Pending.ToString();

                var orderResult = await _orderRepository.CreateAsync(newOrder, cancellationToken);

                return _mapper.Map<CreateOrderResult>(orderResult);
            }
            catch (DomainException ex)
            {
                throw new ApplicationDomainException(ex.Message, ex);
            }            
        }
    }
}