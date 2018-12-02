using ProjectBubbles.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel viewModel;

        public SettingsPage()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel();
            BindingContext = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.InitCommand.Execute(null);
            AppConstants.Logger?.Log("Settings-OnAppearing");
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.SaveSettingsCommand.Execute(null);
            AppConstants.Logger?.Log("Settings-OnDisappearing");
        }
    }
}