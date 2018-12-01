using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProjectBubbles.Models;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using System.IO;
using SQLite;
using Models;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage(string MeetingDate)
        {
            InitializeComponent();

            Item = new Item
            {
                TeamId = "InternalPreview-0.0.1.0",
                MeetingDatePlus = MeetingDate,
                UserName = "",
                Location = "",
                Activity = "work"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {

            // Get the username from the local folder
            try
            {

                string dbPath = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                    "settings.db3");

                SQLiteAsyncConnection database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<Settings>().Wait();

                Settings userSettings = await database.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
                if (userSettings != null)
                {
                    Item.UserName = userSettings.UserName;
                }
                else
                {
                    Item.UserName = "Anonymous";
                }
                //using (var stream = await FileSystem.OpenAppPackageFileAsync("settings.txt"))
                //{
                //    using (var reader = new StreamReader(stream))
                //    {
                //        Item.UserName = await reader.ReadToEndAsync();
                //    }
                //}
            }
            catch (System.Exception)
            {
                throw;
            }


            Item.MeetingDatePlus += $"-{Item.UserName}";
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}