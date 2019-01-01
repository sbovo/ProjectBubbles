using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProjectBubbles.Models;
using ProjectBubbles.Views;
using System.Collections.Generic;
using ProjectBubbles.Services;
using System.Linq;

namespace ProjectBubbles.ViewModels
{

    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }


    public class ItemsNewDesignViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; set; }
        public Command LoadItemsCommand { get; set; }

        public string Date { get; set; }

        public ItemsNewDesignViewModel()
        {
            Date = DateTime.Today.GetUNIVERSALString();
            LoadItems();
        }


        public ItemsNewDesignViewModel(string date)
        {
            Date = date;
            LoadItems();
        }

        private void LoadItems()
        {
            AppConstants.Logger?.Log("ItemsPage");
            Title = Date;
            Items = new ObservableCollection<Item>();
            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(Date));

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                // TODO: sbovo - The item is added to the ObservableCollection even if it is updated (and not added)
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
                AppConstants.Logger?.Log("AddItem to DataStore");
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


                // TODO: Do not hardcode the requests for the x following days
                DateTime Date = DateTime.Now.AddDays(-15);
                for (int i = 0; i < 11; i++)
                {
                    string UniversalStringDate = Date.GetUNIVERSALString();
                    items = await GetItems(UniversalStringDate);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                    Date = Date.AddDays(1);
                }
                var sorted = from item in Items
                             orderby item.MeetingDatePlus
                             group item by item.MeetingDatePlus into itemGroup
                             select new Grouping<string, Item>(itemGroup.Key, itemGroup);
                //create a new collection of groups
                ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);
            }
            catch (Exception ex)
            {
                AppConstants.Logger?.LogError(ex,
                     new Dictionary<string, string> {
                         { "Class", "ItemsNewDesignViewModel" },
                         { "Method", "ExecuteLoadItemsCommand" }
                    });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IEnumerable<Item>> GetItems(string meetingName)
        {
            IEnumerable<Item> itemsForAMeeting = null;
            if (meetingName != null)
            {
                itemsForAMeeting = await DataStore.GetItemsForAMeetingAsync(meetingName, true);
            }
            else
            {
                itemsForAMeeting = await DataStore.GetItemsAsync(true);
            }
            return itemsForAMeeting;
        }
    }
}