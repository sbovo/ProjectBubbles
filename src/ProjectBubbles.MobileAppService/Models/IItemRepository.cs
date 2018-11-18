using System;
using System.Collections.Generic;

namespace ProjectBubbles.Models
{
    public interface IItemRepository
    {
        #region "Default methods for the Interface"
        void Add(Item item);
        void Update(Item item);
        Item Remove(string key);
        Item Get(string id);
        IEnumerable<Item> GetAll();
        #endregion
    }
}
