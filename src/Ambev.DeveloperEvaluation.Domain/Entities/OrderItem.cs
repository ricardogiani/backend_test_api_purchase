using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public long Quantity { get; set; }

        public long UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

    }
}