using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFTodo.ViewModels;
using XFTodo.ViewModels.Base;

namespace XFTodo.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings;
        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainPage));


        }
        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainPage)
            {
                //Do nothing
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateBackFromModalAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopModalAsync();
        }


        public async Task NavigateToModalAsync<TViewModel>(Object parameter, bool animate) where TViewModel : ViewModelBase
        {
            await InternalNavigateToModalAsync(typeof(TViewModel), parameter, animate);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainPage)
            {
                try
                {
                    await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
                    Application.Current.MainPage = new NavigationPage(page);

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

            }
            else
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(page);
                }

                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            }

        }

        protected virtual async Task InternalNavigateToModalAsync(Type viewModelType, object parameter, bool animate)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                //TODO Investigate why await call makes iOS craches
                navigationPage.Navigation.PushModalAsync(page, animate);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

        }
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }
    }
}
