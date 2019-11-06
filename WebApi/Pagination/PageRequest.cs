using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Pagination
{
    public class PageRequest
    {
        public int RequestedPage { get; }
        public int RequestedNumberOfResults { get; }

        public PageRequest(int requestedPage, int requestedNumberOfResults)
        {
            RequestedPage = requestedPage;
            RequestedNumberOfResults = requestedNumberOfResults;
        }
    }
}
