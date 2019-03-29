using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    }
}
