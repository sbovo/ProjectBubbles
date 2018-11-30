using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectBubbles.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {


        

        public SettingsViewModel()
        {
            Title = "Settings";
            SaveSettingsCommand = new Command(async () => await SaveSettings());
        }

        async Task SaveSettings()
        {
            // TODO: Save the username to the "localstate" using Xamarin shared code
            Debug.WriteLine(SavedUserName);

        }

        public ICommand SaveSettingsCommand { get; }
        public string SavedUserName { get; set; }
    }
}