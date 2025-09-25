using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Get paginated customers
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> <summary>
        Task<(IEnumerable<Customer> Products, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}