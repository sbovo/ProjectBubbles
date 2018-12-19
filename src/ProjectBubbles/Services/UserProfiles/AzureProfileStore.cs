using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Models;
using Newtonsoft.Json;
using ProjectBubbles.Models;
using Xamarin.Forms;

namespace ProjectBubbles.Services
{
    public class AzureProfileStore : IProfileStore<Profile>
    {
        HttpClient client;
        IEnumerable<Item> items;


        public AzureProfileStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{AppConstants.AzureBackendUrl}/");

            items = new List<Item>();
        }

        public async Task<Profile> GetItemAsync(string UserName)
        {
            if (UserName != null)
            {
                var json = await client.GetStringAsync($"api/profile/{UserName}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Profile>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Profile profile)
        {
            if (profile == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(profile);

            var response = await client.PostAsync($"api/profile", new StringContent(serializedItem, Encoding.UTF8, "application/json"));


            AppConstants.Logger?.Log("AzureProfileStore-AddItemAsync",
                new Dictionary<string, string> { { "Result", response.StatusCode.ToString() } });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Profile item)
        {
            // TODO: Implement
            return false;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            // TODO: Implement
            return false;
        }
    }
}