using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using System;
using SQLite;
using Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ProjectBubbles.Services;

namespace ProjectBubbles.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
       

        public SettingsViewModel()
        {
            Title = "Settings";
           
            InitCommand = new Command(async () => await ExecuteInitCommand());
            SaveSettingsCommand = new Command(async () => await SaveSettings());

           
        }

        private async Task ExecuteInitCommand()
        {

            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "settings.db3");

            LocalDatabase = new SQLiteAsyncConnection(dbPath);
            LocalDatabase.CreateTableAsync<Settings>().Wait();

            LocalSettings = new Settings { ID = 0, UserName = "User" + Guid.NewGuid().ToString() };

            App.Logger?.Log("Settings-LoadingFromSQLite");
            Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
            if (userSettings != null)
            {
                LocalSettings = userSettings;
            }
        }

        async Task SaveSettings()
        {
            try
            {
                App.Logger?.Log("Settings-LoadingFromSQLite");
                Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
                if (userSettings == null)
                {
                    App.Logger?.Log("Settings-InsertSQLite");
                    await LocalDatabase.InsertAsync(LocalSettings);
                }
                else
                {
                    App.Logger?.Log("Settings-UpdateSQLite");

                    await LocalDatabase.UpdateAsync(LocalSettings);
                }
            }
            catch (System.Exception)
            {
                throw;
            }


        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand InitCommand { get; }

        public SQLiteAsyncConnection LocalDatabase { get; set; }


        private Settings localSetting;
        public Settings LocalSettings
        {
            get { return localSetting; }
            set { SetProperty(ref localSetting, value); }
        }
    }
}