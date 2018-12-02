using ProjectBubbles.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectBubbles
{
    public static class AppConstants
    {
        //public static string AzureBackendUrl = "http://localhost:5000/";
        public static string AzureBackendUrl = "https://bubblesappservice.azurewebsites.net/";
        public static bool UseMockDataStore = false;
        public static ILogger Logger;
    }
}
