using System;
using System.Net.Http;

namespace Yangaroo.Web.Http
{
    /// <summary>
    /// Provides methods for building HTTP responses in ASP.NET WebAPI.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessage WithLocationHeader(this HttpResponseMessage response, string uri)
        {
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
