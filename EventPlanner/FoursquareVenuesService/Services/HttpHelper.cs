using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoursquareVenuesService.Services
{
    /// <summary>
    /// Helper for foursquare http requests.
    /// </summary>
    public static class HttpHelper<T> where T : class
    {
        /// <summary>
        /// Gets result from given URI.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        /// <param name="uri">URI with request.</param>
        /// <returns>Gets result of type T from request.</returns>
        public static async Task<T> GetResult(HttpClient httpClient, string uri)
        {
            var result = await httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
