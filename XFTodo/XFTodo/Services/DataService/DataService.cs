using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XFTodo.Helpers;
using XFTodo.Models;

namespace XFTodo.Services.DataService
{
    public class DataService : IDataService
    {
        const string BaseUrl = "https://mtltodoapi.azurewebsites.net/api/";
        private const string EndPoint = "todo";
        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            try
            {
                var response = await HttpClientHelper.GetAsync(BaseUrl + EndPoint);
                return JsonConvert.DeserializeObject<List<TodoItem>>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }

            return null;
        }

        public async Task<TodoItem> PostTodoItemAsync(TodoItem item)
        {
            try
            {
                var response = await HttpClientHelper.PostAsync(BaseUrl + EndPoint, item); 
                return JsonConvert.DeserializeObject<TodoItem>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }

            return null;

        }

        public async Task<bool> PutTodoItemAsync(TodoItem item)
        {
            var result = false;

            try
            {
                 result = await HttpClientHelper.PutAsync($"{BaseUrl}{EndPoint}/{item.Id}", item);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }

            return result;
        }

        public async Task<bool> DeleteTodoItemAsync(long id)
        {
            var result = false;
            try
            {
                var response = await HttpClientHelper.DeleteAsync($"{BaseUrl}{EndPoint}/{id}");

                if (response == HttpStatusCode.NoContent) result = true;

                Debug.WriteLine($"Deletion operation status code: {response}");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }

            return result;
        }
    }
}
