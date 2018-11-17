using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBubbles.AzureTableDataAccessLayer
{
    public class TableItem : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableItem"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public TableItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableItem"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="teamId">The team id.</param>
        /// <param name="meetingDatePlus">The name of the meeting including the date</param>
        public TableItem(string teamId, string meetingDatePlus)
        {
            PartitionKey = teamId;
            RowKey = meetingDatePlus;
        }






        public string TeamId { get; set; }
        public string MeetingDatePlus { get; set; }
        public ParticipationEnum Participation { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Activity { get; set; }
    }
}
