using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBubbles.AzureTableDataAccessLayer
{
    public class ProfileItem : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableItem"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public ProfileItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableItem"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="UserId">The User id.</param>
        /// <param name="UserName">The user's pseudo</param>
        public ProfileItem(string UserId, string UserName)
        {
            PartitionKey = UserId;
            RowKey = UserName;
        }

        
        public string UserId
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }
        public string Username
        {
            get => RowKey;
            set => RowKey = value;
        }

        public string PhotoBase64Encoded { get; set; }
    }
}
