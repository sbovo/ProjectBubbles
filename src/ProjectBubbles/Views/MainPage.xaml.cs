using ProjectBubbles.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        ILogger Logger { get; }

        public MainPage()
        {
            InitializeComponent();

            Logger = DependencyService.Resolve<ILogger>();
            CreatePages();
        }

        private void CreatePages()
        {
            Logger?.Log("MainPage");
            DateTime Date = DateTime.Now;
            //TODO: we need a better way to do that
            for (int i = 0; i < 11; i++)
            {
                string UniversalStringDate = Date.GetUNIVERSALString();
                ItemsPage PageDay = new ItemsPage(UniversalStringDate);
                NavigationPage P = new NavigationPage(PageDay);
                P.Title = UniversalStringDate;
                this.Children.Add(P);
                Date = Date.AddDays(1);
            }
        }
    }
}