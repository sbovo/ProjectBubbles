using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.AzureTableDataAccessLayer;
using System.Threading.Tasks;

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
            //// All users are in the same team
            //TableItem item1 = new TableItem {
            //    TeamId = Team, MeetingDatePlus = "2019-12-01-chmaneu", UserName = "chmaneu",
            //    Location = "EOS", Activity = "Azure Evergreen" };
            //TableItem item2 = new TableItem {
            //    TeamId = Team, MeetingDatePlus = "2019-12-01-sypontor", UserName = "sypontor",
            //    Location = "Home", Activity = "Git forever" };
            //TableItem item3 = new TableItem {
            //    TeamId = Team, MeetingDatePlus = "2019-12-01-sbovo", UserName = "sbovo",
            //    Location = "EOS", Activity = "HoloLens dev" };


            //item1 = SamplesUtils.InsertOrMergeEntityAsync(table, item1).GetAwaiter().GetResult();
            //item2 = SamplesUtils.InsertOrMergeEntityAsync(table, item2).GetAwaiter().GetResult();
            //item3 = SamplesUtils.InsertOrMergeEntityAsync(table, item3).GetAwaiter().GetResult();
        }

        public Item Get(string id)
        {
            return items[id];
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            List<Item> listItems = new List<Item>();
            // TODO: Get all items for a PartitionKey and a RowLey starting with a date (ex: "2019-12-01-blabla")
            //SamplesUtils.RetrieveEntityUsingPointQueryAsync(table, "28881b7a-d3e2-4eae-8687-0c4f4c5e6107", "2019-12-01").GetAwaiter().GetResult();
            var list = await SamplesUtils.GetList(table, "InternalPreview-0.0.1.0", null);
            foreach (TableItem item in list)
            {
                listItems.Add(new Item { TeamId = item.TeamId, MeetingDatePlus = item.MeetingDatePlus, UserName = item.UserName,
                    Participation = item.Participation, Activity = item.Activity, Location = item.Location});
            }
            return listItems;// items.Values;
        }

        public async Task<IEnumerable<Item>> GetAllForADate(string meetingDate)
        {
            List<Item> listItems = new List<Item>();
            // TODO: Get all items for a PartitionKey and a RowLey starting with a date (ex: "2019-12-01-blabla")
            //SamplesUtils.RetrieveEntityUsingPointQueryAsync(table, "28881b7a-d3e2-4eae-8687-0c4f4c5e6107", "2019-12-01").GetAwaiter().GetResult();
            var list = await SamplesUtils.GetList(table, "InternalPreview-0.0.1.0", meetingDate);
            foreach (TableItem item in list)
            {
                    listItems.Add(new Item
                    {
                        TeamId = item.TeamId,
                        MeetingDatePlus = item.MeetingDatePlus,
                        UserName = item.UserName,
                        Participation = item.Participation,
                        Activity = item.Activity,
                        Location = item.Location
                    });
            }
            return listItems;// items.Values;
        }

        public async Task Add(Item item)
        {
            TableItem tableItem = new TableItem
            {
                TeamId = item.TeamId,
                MeetingDatePlus = item.MeetingDatePlus,
                Participation = item.Participation,
                UserName = item.UserName,
                Location = item.Location,
                Activity = item.Activity
            };

            tableItem = await SamplesUtils.InsertOrMergeEntityAsync(table, tableItem);
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
