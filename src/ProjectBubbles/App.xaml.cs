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

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=bbaa52cb-718e-4c6f-91b2-ceee96cce5be;" 
                + "uwp={Your UWP App secret here};" 
                + "ios={Your iOS App secret here}",
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
