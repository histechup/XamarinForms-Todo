using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFTodo.Models;
using XFTodo.Services.DataService;
using XFTodo.ViewModels.Base;

namespace XFTodo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            TodoItems = new ObservableCollection<TodoItem>();
            Task.Run(async () => { await ExecuteRefreshCommand(); });

        }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        private TodoItem _selectedTodoItem;

        public TodoItem SelectedTodoItem
        {
            get => _selectedTodoItem;
            set
            {
                _selectedTodoItem = value;
                OnPropertyChanged();

                if (_selectedTodoItem == null) return;
                NavigateToPostDetail();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await ExecuteRefreshCommand());
        public ICommand AddNewTodoItemCommand => new Command(NavigateToPostDetail);

       public async Task ExecuteRefreshCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                TodoItems.Clear();
                var newTodoItems = await _dataService.GetTodoItemsAsync();
                foreach (var item in newTodoItems)
                {
                    TodoItems.Add(item);
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

        

        async void NavigateToPostDetail()
        {
            await NavigationService.NavigateToAsync<TodoItemViewModel>();
        }


    }
}
