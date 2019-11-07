using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CommitViewer
{
    static class GitHubClient
    {
        static HttpClient client = new HttpClient();
    }
}
