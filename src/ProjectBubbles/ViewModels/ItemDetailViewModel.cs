using System;
using ProjectBubbles.Models;
using ProjectBubbles.Services;
using Xamarin.Forms;

namespace ProjectBubbles.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            AppConstants.Logger?.Log("ItemDetailPage");
            Title = item?.UserName;
            Item = item;
        }
    }
}
