using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.AzureTableDataAccessLayer;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    public class AzureProfileItemRepository : IProfileRepository
    {
        // TODO: Work with Azure Table and not the in Memory ConcurrentDictionary
        private static ConcurrentDictionary<string, Profile> items =
            new ConcurrentDictionary<string, Profile>();

        CloudTable table;




        public AzureProfileItemRepository()
        {
            // Create or reference an existing table
            table = Common.CreateTableAsync("UserProfiles").GetAwaiter().GetResult();
        }

        public Profile Get(string id)
        {
            return items[id];
        }

        public async Task Add(Profile item)
        {
            ProfileItem profileItem = new ProfileItem
            {
                UserId = item.UserId,
                Username = item.UserName,
                PhotoBase64Encoded = item.PhotoBase64Encoded
            };

            profileItem = await SamplesUtils.InsertOrMergeEntityAsync(table, profileItem);
        }

        public Profile Find(string id)
        {
            Profile p;
            items.TryGetValue(id, out p);

            return p;
        }

        public Profile Remove(string id)
        {
            Profile p;
            items.TryRemove(id, out p);

            return p;
        }

        public void Update(Profile p)
        {
            items[p.UserId] = p;
        }
    }
}
