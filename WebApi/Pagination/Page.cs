using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Pagination
{
    public class Page<T> : IPage<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}
