using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProjectBubbles.Models;
using Helpers;

namespace ProjectBubbles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                TeamId = "InternalPreview-0.0.1.0",
                MeetingDatePlus = DateHelper.GetUNIVERSALStringFromDate(DateTime.Today),
                UserName = "",
                Location = "",
                Activity = "work"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.MeetingDatePlus += $"-{Item.UserName}";
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}