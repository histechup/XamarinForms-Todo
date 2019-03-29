using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFTodo.Services.NavigationService;
using XFTodo.ViewModels.Base;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFTodo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            BuildDependencies();
            InitNavigation();
        }

        public void BuildDependencies()
        {
            Locator.Instance.Build();
        }
        private async void InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
