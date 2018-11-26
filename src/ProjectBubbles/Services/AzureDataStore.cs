using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Models;
using Newtonsoft.Json;
using ProjectBubbles.Helpers;
using ProjectBubbles.Models;

namespace ProjectBubbles.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        HttpClient client;
        IEnumerable<Item> items;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Item>();
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));


            LogHelper.Log("AzureDataStore-AddItemAsync", 
                new Dictionary<string, string> {{"Result",  response.StatusCode.ToString() }});
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.TeamId == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.TeamId}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/item");
                Result r = await Task.Run(() => JsonConvert.DeserializeObject<Result>(json));
                items = r.result;
            }

            return items;
        }

        public async Task<IEnumerable<Item>> GetItemsForAMeetingAsync(string meetingName, bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                LogHelper.Log("GetItemsForAMeetingAsync");
                var json = await client.GetStringAsync($"api/items/{meetingName}");
                Result r = await Task.Run(() => JsonConvert.DeserializeObject<Result>(json));
                items = r.result;
            }

            return items;
        }
    }
}