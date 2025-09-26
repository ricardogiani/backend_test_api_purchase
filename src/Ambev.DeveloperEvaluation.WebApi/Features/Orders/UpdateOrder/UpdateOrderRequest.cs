using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public IEnumerable<OrderItemRequest> OrderItems { get; set; }
    }
}