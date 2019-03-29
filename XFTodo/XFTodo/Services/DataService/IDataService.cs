using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XFTodo.Models;

namespace XFTodo.Services.DataService
{
    public interface IDataService
    {
        Task<List<TodoItem>> GetTodoItemsAsync();

        Task<TodoItem> PostTodoItemAsync(TodoItem item);

        Task<bool> PutTodoItemAsync(TodoItem item);

        Task<bool> DeleteTodoItemAsync(long id);
    }
}
