using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    public interface IProfileRepository
    {
        #region "Default methods for the Interface"
        Task Add(Item item);
        void Update(Item item);
        Item Remove(string key);
        Item Get(string id);
        #endregion
    }
}
