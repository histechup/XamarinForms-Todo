using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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



        /// <summary>
        /// HttpClient POST
        /// </summary>
        /// <param name="uri">Query string</param>
        /// <param name="data">data to post</param>
        /// <returns>Json object or null</returns>
        public static async Task<string> PostAsync(string uri, object data)
        {

            string responseContent = null;
            try
            {
                HttpClient httpClient = new HttpClient();

                //Get Auth Token from Cache
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer authToken");

                var json = JsonConvert.SerializeObject(data);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result; 
                return responseContent;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"HttpClientHelper Exception: {e.Message}");
            }


            return responseContent;


        }




        /// <summary>
        /// HttpClient PUT
        /// </summary>
        /// <param name="uri">Query string</param>
        /// <param name="data">data to post</param>
        /// <returns>Json object or null</returns>
        public static async Task<bool> PutAsync(string uri, object data)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer authToken");

            var json = JsonConvert.SerializeObject(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;
            else return false;



        }



        /// <summary>
        /// HttpClient PUT
        /// </summary>
        /// <param name="uri">Query string</param>
        /// <returns>Json object or null</returns>
        public static async Task<HttpStatusCode> DeleteAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();

            // httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer authToken ");

            var response = await httpClient.DeleteAsync(uri);
            return response.StatusCode;


        }

    }
}
