using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Order : BaseEntity
    {

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public string Status { get; set; }

        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        /// <summary>
        /// User Id Operator
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Gets the date and time when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the date and time of the last update order
        /// </summary>
        public DateTime? UpdatedAt { get; set; }


        public (bool Success, string Message) AddItem(OrderItem item)
        {
            _orderItems.Add(item);

            TotalAmount = _orderItems.Sum(x => x.UnitPrice * x.Quantity);

            return (Success: true, Message: "Success");
        }

        public bool RemoveItem(OrderItem item)
        {
            var count = _orderItems.RemoveAll(x => x.ProductId == item.ProductId);

            TotalAmount = _orderItems.Sum(x => x.UnitPrice * x.Quantity);

            return (count > 0);
        }
    }
}