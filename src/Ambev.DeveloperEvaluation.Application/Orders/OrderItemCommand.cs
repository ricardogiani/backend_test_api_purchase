using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders
{
    public class OrderItemCommand
    {
        public Guid ProductId { get; set; }

        public long Quantity { get; set; }

        public long UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}