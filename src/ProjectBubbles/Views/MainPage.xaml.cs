using Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            CreatePages();
        }

        private void CreatePages()
        {
            DateTime Date = DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                string UniversalStringDate = DateHelper.GetUNIVERSALString(Date);
                ItemsPage PageDay = new ItemsPage(UniversalStringDate);
                NavigationPage P = new NavigationPage(PageDay);
                P.Title = UniversalStringDate;
                this.Children.Add(P);
                Date = Date.AddDays(1);
            }
        }
    }
}