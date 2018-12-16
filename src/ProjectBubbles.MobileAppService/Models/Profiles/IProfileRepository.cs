using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    public interface IProfileRepository
    {
        #region "Default methods for the Interface"
        Task Add(Profile profile);
        void Update(Profile profile);
        Profile Remove(string id);
        Profile Get(string id);
        #endregion
    }
}
