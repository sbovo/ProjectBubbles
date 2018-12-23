using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using System;
using SQLite;
using ProjectBubbles.Services;
using ProjectBubbles.Models;
using Plugin.Media;

namespace ProjectBubbles.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public IProfileStore<Profile> DataStore => DependencyService.Get<IProfileStore<Profile>>() ?? null;

        public SettingsViewModel()
        {
            Title = "Settings";

            InitCommand = new Command(async () => await ExecuteInitCommand());
            SaveSettingsCommand = new Command(async () => await SaveSettings());
            ChangePictureCommand = new Command(async () => await ChangePicture());
            UserNameChangeCommand = new Command(async () => await UserNameChange());

        }


        private async Task UserNameChange()
        {
            await Load();
            OnPropertyChanged("LocalSettings");
        }

        private async Task ChangePicture()
        {
            //https://github.com/jamesmontemagno/MediaPlugin
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                System.Diagnostics.Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;
            var bytes = default(byte[]);
            using (var streamReader = new StreamReader(file.Path))
            {
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }
            }
            string _b64 = Convert.ToBase64String(bytes);
            // We have the correct base64 encoded image in _b64
            LocalSettings.Photo = _b64;
            //ImgSource = LocalSettings.Photo;
            Task.Run(async () =>
            {
                await SaveSettings();
            }).Wait();
            OnPropertyChanged("LocalSettings");
            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});
        }

        private async Task ExecuteInitCommand()
        {
            // TODO: Refactor the SQLite code
            // TODO: Close the SQLite database
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "settings.db3");
            LocalDatabase = new SQLiteAsyncConnection(dbPath);
            LocalDatabase.CreateTableAsync<Settings>().Wait();

            LocalSettings = new Settings { ID = 0, UserName = "User" + Guid.NewGuid().ToString() };
            AppConstants.Logger?.Log("Settings-LoadingFromSQLite");
            Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstAsync();
            if (userSettings != null)
            {
                LocalSettings = userSettings;
                //ImgSource = LocalSettings.Photo;
                AppConstants.Logger?.Log("Settings-LoadingFromSQLite-" + userSettings.UserName);
                await Load();
            }
        }

        private async Task Load()
        {
            Profile profileFromAzure = null;
            try
            {
                profileFromAzure = await DataStore.GetItemAsync(LocalSettings.UserName);
            }
            catch (Exception)
            {

                
            }
             
            if (profileFromAzure == null)
            {
                Profile p = new Profile
                {
                    UserId = "1",
                    UserName = LocalSettings.UserName,
                    PhotoBase64Encoded = "not set"
                };
                await DataStore.AddItemAsync(p);
            }
            else
            {
                LocalSettings = new Settings { ID=0,
                    UserName = profileFromAzure.UserName,
                    Photo = profileFromAzure.PhotoBase64Encoded };
            }
        }

        async Task SaveSettings()
        {
            try
            {
                AppConstants.Logger?.Log("Settings-LoadingFromSQLite");
                Settings userSettings = await LocalDatabase.Table<Settings>().Where(i => i.ID == 0).FirstOrDefaultAsync();
                if (userSettings == null)
                {
                    AppConstants.Logger?.Log("Settings-InsertSQLite");
                    AppConstants.Logger?.Log("Settings-InsertSQLite-" + LocalSettings.UserName);
                    await LocalDatabase.InsertAsync(LocalSettings);
                }
                else
                {
                    AppConstants.Logger?.Log("Settings-LoadingFromSQLite-" + userSettings.UserName);
                    AppConstants.Logger?.Log("Settings-UpdateSQLite");
                    AppConstants.Logger?.Log("Settings-UpdateSQLite-" + LocalSettings.UserName);
                    await LocalDatabase.UpdateAsync(LocalSettings);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            try
            {
                AppConstants.Logger?.Log("Settings-SaveSettings-Azure-AddItemAsync" + LocalSettings.UserName);
                Profile p = new Profile { UserId = "1", UserName = LocalSettings.UserName, PhotoBase64Encoded = LocalSettings.Photo };
                await DataStore.AddItemAsync(p);
            }
            catch (Exception ex)
            {
                AppConstants.Logger?.Log($"EXCEPTION: {ex.Message}");
            }


        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand InitCommand { get; }
        public ICommand ChangePictureCommand { get; }
        public ICommand UserNameChangeCommand { get; }

        public SQLiteAsyncConnection LocalDatabase { get; set; }


        private Settings localSettings;
        public Settings LocalSettings
        {
            get { return localSettings; }
            set { SetProperty(ref localSettings, value); }
        }

        //private ImageSource imgSource;
        //public ImageSource ImgSource
        //{
        //    get { return imgSource; }
        //    set { SetProperty(ref imgSource, value); }
        //}

        public Extensions.ImageAzureExtension PhotoImageSource { get; set; }
        public Extensions.ImageCloudExtension PhotoImageSource2 { get; set; }

    }
}