using System;
using ProjectBubbles.Models;
using ProjectBubbles.Services;
using Xamarin.Forms;

namespace ProjectBubbles.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        private ILogger Logger { get;  }
        public ItemDetailViewModel(Item item = null)
        {
            Logger = DependencyService.Resolve<ILogger>();

            Logger?.Log("ItemDetailPage");
            Title = item?.UserName;
            Item = item;
        }
    }
}
