using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    public class InMemoryItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        public InMemoryItemRepository()
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

        public async Task<IEnumerable<Item>> GetAll()
        {
            return items.Values;
        }

        public async Task Add(Item item)
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

        public Task<IEnumerable<Item>> GetAllForADate(string meetingDate)
        {
            throw new NotImplementedException();
        }
    }
}
