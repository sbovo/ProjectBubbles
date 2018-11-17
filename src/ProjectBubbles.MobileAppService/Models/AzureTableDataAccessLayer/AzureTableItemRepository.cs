using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ProjectBubbles.Models
{
    public class AzureTableItemRepository : IItemRepository
    {
        // TODO: Work with Azure Table and not the in Memory ConcurrentDictionary
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        public AzureTableItemRepository()
        {
            string Team = Guid.NewGuid().ToString();
            // All users are in the same team
            Add(new Item { TeamId = Team, MeetingDatePlus = "2019-12-01", UserName = "chmaneu", Location = "EOS", Activity = "Azure Evergreen" });
            Add(new Item { TeamId = Team, MeetingDatePlus = "2019-12-01", UserName = "sypontor", Location = "Home", Activity = "Git forever" });
            Add(new Item { TeamId = Team, MeetingDatePlus = "2019-12-01", UserName = "sbovo", Location = "EOS", Activity = "HoloLens dev" });
        }

        public Item Get(string id)
        {
            return items[id];
        }

        public IEnumerable<Item> GetAll()
        {
            return items.Values;
        }

        public void Add(Item item)
        {
            item.TeamId = Guid.NewGuid().ToString();
            items[item.TeamId] = item;
        }

        public Item Find(string id)
        {
            Item item;
            items.TryGetValue(id, out item);

            return item;
        }

        public Item Remove(string id)
        {
            Item item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(Item item)
        {
            items[item.TeamId] = item;
        }
    }
}
