using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XFTodo.ViewModels.Base;

namespace XFTodo.Services.NavigationService
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToModalAsync<TViewModel>(object parameter = null, bool animate = true) where TViewModel : ViewModelBase;

        Task NavigateBackAsync();

        Task NavigateBackFromModalAsync();
    }
}
