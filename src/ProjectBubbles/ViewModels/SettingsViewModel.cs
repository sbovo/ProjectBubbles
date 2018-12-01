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

            Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
            if (userSettings != null)
            {
                LocalSettings = userSettings;
            }
            else
            {
                LocalSettings = new Settings { ID = 0, UserName = "User" + Guid.NewGuid().ToString() };
            }
        }

        async Task SaveSettings()
        {
            try
            {
                Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
                if (userSettings == null)
                {
                    await LocalDatabase.InsertAsync(LocalSettings);
                }
                else
                {
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