using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public Guid CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Guid BranchId { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderItemCommand> OrderItems { get; set; }

        public Guid UserId { get; set; }
    }
}