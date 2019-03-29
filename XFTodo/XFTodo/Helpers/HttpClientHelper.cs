using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XFTodo.Helpers
{
    public class HttpClientHelper
    {
        /// <summary>
        /// HttpClient GET
        /// </summary>
        /// <param name="uri">Query string</param>
        /// <returns>String</returns>
        public static async Task<string> GetAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            var responseContent = response.Content.ReadAsStringAsync().Result;

            return responseContent;
        }

    }
}
