using System;
using ProjectBubbles.Helpers;
using ProjectBubbles.Models;

namespace ProjectBubbles.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            LogHelper.Log("ItemDetailPage");
            Title = item?.UserName;
            Item = item;
        }
    }
}
