using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ProjectBubbles.Models
{
    public class ItemRepository : IItemRepository
    {
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        public ItemRepository()
        {
            Add(new Item { Id = Guid.NewGuid().ToString(), Date = "2019-12-01", Login = "chmaneu", Location = "EOS", Activity = "Azure Evergreen" });
            Add(new Item { Id = Guid.NewGuid().ToString(), Date = "2019-12-01", Login = "sypontor", Location = "Home", Activity = "Git forever" });
            Add(new Item { Id = Guid.NewGuid().ToString(), Date = "2019-12-01", Login = "sbovo", Location = "EOS", Activity = "HoloLens dev" });
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
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
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
            items[item.Id] = item;
        }
    }
}
