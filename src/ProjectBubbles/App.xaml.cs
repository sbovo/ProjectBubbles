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
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //public static string AzureBackendUrl = "http://localhost:5000/";
        public static string AzureBackendUrl = "https://bubblesappservice.azurewebsites.net/";
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            DependencyService.Register<ILogger, AppCenterLogger>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //TODO: Need to be in a configuration file
            AppCenter.Start("android=bbaa52cb-718e-4c6f-91b2-ceee96cce5be;" 
                + "uwp=bc566608-1fd9-4dc5-91fe-8b532cd1cd16;"
                + "ios=59ec4157-e51c-4c2a-bc94-f093558c6605",
                typeof(Analytics), typeof(Crashes));
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
