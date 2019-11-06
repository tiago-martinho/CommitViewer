using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Pagination
{
    public interface IPage<out T>
    {
        int PageNumber { get; }

        int PageSize { get; }

        IEnumerable<T> Content { get; }
    }
}
