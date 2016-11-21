using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoursquareVenuesService.Services
{
    /// <summary>
    /// Helper for foursquare http requests.
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Gets result from given URI.
        /// </summary>
        /// <param name="uri">URI with request.</param>
        /// <returns>Gets result of type T from request.</returns>
        public static async Task<T> GetResult<T>(string uri)
             where T : class
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<T>(result);
            }   
        }

        /// <summary>
        /// Gets result from given URI.
        /// </summary>
        /// <param name="uri">URI with request.</param>
        /// <param name="type">Anonymous type</param>
        /// <returns>Gets result of type T from request.</returns>
        public static async Task<T> GetResult<T>(string uri, T type)
            where T : class
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), type);
                }

                var x = await response.Content.ReadAsStringAsync();

                return null;
            }
        }
    }
}
