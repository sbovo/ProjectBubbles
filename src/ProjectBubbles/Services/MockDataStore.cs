using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBubbles.Models;

namespace ProjectBubbles.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
            new Item { TeamId = Guid.NewGuid().ToString(), MeetingDatePlus = "2019-12-01", UserName = "chmaneu", Location = "EOS - Mock", Activity = "Azure Evergreen" },
            new Item { TeamId = Guid.NewGuid().ToString(), MeetingDatePlus = "2019-12-01", UserName = "sypontor", Location = "Home - Mock", Activity = "Git forever" },
            new Item { TeamId = Guid.NewGuid().ToString(), MeetingDatePlus = "2019-12-01", UserName = "sbovo", Location = "EOS - Mock", Activity = "HoloLens dev" },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.TeamId == item.TeamId).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.TeamId == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.TeamId == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}