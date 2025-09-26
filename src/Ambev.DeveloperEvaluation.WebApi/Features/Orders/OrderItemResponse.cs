using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders
{
    public class OrderItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Dicount { get; set; }
        public decimal TotalAmount { get; set; }

    }
}