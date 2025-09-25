using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<OrderItem> OrderItems { get; set; }
        
        /// <summary>
        /// User Id Operator
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}