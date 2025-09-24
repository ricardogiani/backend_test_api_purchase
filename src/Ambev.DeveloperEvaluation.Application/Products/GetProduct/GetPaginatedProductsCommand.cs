using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetPaginatedProductsCommand  : IRequest<GetPaginatedProductsResult>
    {
        private int pageNumber;
        private int pageSize;

        public GetPaginatedProductsCommand(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }
    }
}