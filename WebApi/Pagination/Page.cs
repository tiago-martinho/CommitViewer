using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Pagination
{
    public class Page<T> : IPage<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalElements { get;  }
        public int TotalPages { get; }
        public IEnumerable<T> Content { get; }

        public Page(int pageNumber, int pageSize, int totalElements, IEnumerable<T> content)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalElements = totalElements;
            TotalPages = (totalElements + pageSize - 1) / pageSize; // this is way more efficient than using Math.Ceiling and casting arguments to double and result back to int
            Content = content;
        }
    }
}
