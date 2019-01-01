using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProjectBubbles.Models;
using ProjectBubbles.Views;
using ProjectBubbles.ViewModels;
using Microsoft.AppCenter.Analytics;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPageNewDesign : ContentPage
    {
        ItemsNewDesignViewModel viewModel;

        public ItemsPageNewDesign()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsNewDesignViewModel();
        }

        public ItemsPageNewDesign(string id)
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsNewDesignViewModel(id);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(viewModel.Date)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ItemsGrouped.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}