using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBubbles.Services
{
    public interface IProfileStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string UserName);
    }
}
