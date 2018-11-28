using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProjectBubbles.Models;
using ProjectBubbles.Views;
using System.Collections.Generic;
using ProjectBubbles.Helpers;

namespace ProjectBubbles.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public string Date { get; set; }

        public ItemsViewModel()
        {
            Date = DateTime.Today.GetUNIVERSALString();
            LoadItems();
        }


        public ItemsViewModel(string date)
        {
            Date = date;
            LoadItems();
        }

        private void LoadItems()
        {
            LogHelper.Log("ItemsPage");
            Title = Date;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(Date));

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                // TODO: sbovo - The item is added to the ObservableCollection even if it is updated (and not added)
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
                LogHelper.Log("AddItem to DataStore");
            });
        }

        async Task ExecuteLoadItemsCommand(string meetingName)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                IEnumerable<Item> items = null;
                if (meetingName != null)
                {
                    items = await DataStore.GetItemsForAMeetingAsync(meetingName, true);
                }
                else
                {
                    items = await DataStore.GetItemsAsync(true);
                }
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}