using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFTodo.Models;
using XFTodo.Services.DataService;
using XFTodo.ViewModels.Base;

namespace XFTodo.ViewModels
{
    public class TodoItemViewModel: ViewModelBase
    {
        private readonly IDataService _dataService;

        public TodoItemViewModel(IDataService dataService)
        {
            _dataService = dataService;

            TodoItem = Locator.Instance.Resolve<MainViewModel>().SelectedTodoItem != null ? Locator.Instance.Resolve<MainViewModel>().SelectedTodoItem : new TodoItem();
        }

        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get => _todoItem;
            set { _todoItem = value; OnPropertyChanged(); }
        }
        public string PageTitle => TodoItem.Name ?? "Todo Item";

        public ICommand SaveCommand => new Command(async () => { await ExecuteSaveTodoItemCommand(); });
        public ICommand DeleteCommand => new Command(async () => { await ExecuteDeleteCommand(); });

        async Task ExecuteSaveTodoItemCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (string.IsNullOrEmpty(TodoItem.Name))
            {
                TodoItem.Name = "N/A";

            }

            try
            {
                if (TodoItem.Id == 0)
                {
                    var result = await _dataService.PostTodoItemAsync(TodoItem);
                    Locator.Instance.Resolve<MainViewModel>().TodoItems.Add(result);
                }
                else
                {
                  var result =  await _dataService.PutTodoItemAsync(TodoItem);
                    if (result)
                    {
                        await Locator.Instance.Resolve<MainViewModel>().ExecuteRefreshCommand();
                    }
                    else
                    {
                        Debug.WriteLine("Something unexpected happened in PUT Operation");
                    }

                }
             

                await NavigationService.NavigateBackAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

           
            try
            {
                var result = await _dataService.DeleteTodoItemAsync(TodoItem.Id);

                if (result)
                {
                    Locator.Instance.Resolve<MainViewModel>().TodoItems.Remove(TodoItem);

                    await NavigationService.NavigateBackAsync();
                }
                else
                {
                    Debug.WriteLine("Something unexpected happened in DELETE Operation");
                }
               
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw new Exception();
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
