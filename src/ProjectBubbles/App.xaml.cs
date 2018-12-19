using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectBubbles.Services;
using ProjectBubbles.Views;
// AppCenter Telemetry and diagnostics
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProjectBubbles
{
    public partial class App : Application
    {
       

        public App()
        {
            InitializeComponent();

            if (AppConstants.UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            DependencyService.Register<AzureProfileStore>();
            DependencyService.Register<ILogger, AppCenterLogger>();
            AppConstants.Logger = DependencyService.Resolve<ILogger>();

            MainPage = new MainPage();
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
