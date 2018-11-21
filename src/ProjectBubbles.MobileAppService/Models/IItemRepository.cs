using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    public interface IItemRepository
    {
        #region "Default methods for the Interface"
        Task Add(Item item);
        void Update(Item item);
        Item Remove(string key);
        Item Get(string id);
        Task<IEnumerable<Item>> GetAll();
        Task<IEnumerable<Item>> GetAllForADate(string meetingDate);
        #endregion
    }
}
