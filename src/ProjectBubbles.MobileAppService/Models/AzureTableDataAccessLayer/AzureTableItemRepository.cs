using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.AzureTableDataAccessLayer;

namespace ProjectBubbles.Models
{
    public class AzureTableItemRepository : IItemRepository
    {
        // TODO: Work with Azure Table and not the in Memory ConcurrentDictionary
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        CloudTable table;




        public AzureTableItemRepository()
        {
            // Create or reference an existing table
            table = Common.CreateTableAsync("TeamsEventsAndParticipations").GetAwaiter().GetResult();

            string Team = Guid.NewGuid().ToString();
            // All users are in the same team
            TableItem item1 = new TableItem {
                TeamId = Team, MeetingDatePlus = "2019-12-01-chmaneu", UserName = "chmaneu",
                Location = "EOS", Activity = "Azure Evergreen" };
            TableItem item2 = new TableItem {
                TeamId = Team, MeetingDatePlus = "2019-12-01-sypontor", UserName = "sypontor",
                Location = "Home", Activity = "Git forever" };
            TableItem item3 = new TableItem {
                TeamId = Team, MeetingDatePlus = "2019-12-01-sbovo", UserName = "sbovo",
                Location = "EOS", Activity = "HoloLens dev" };


            item1 = SamplesUtils.InsertOrMergeEntityAsync(table, item1).GetAwaiter().GetResult();
            item2 = SamplesUtils.InsertOrMergeEntityAsync(table, item2).GetAwaiter().GetResult();
            item3 = SamplesUtils.InsertOrMergeEntityAsync(table, item3).GetAwaiter().GetResult();
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
