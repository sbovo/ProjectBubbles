using System;

namespace ProjectBubbles.Models
{
    public class Item
    {
        public string Id { get; set; }
        /// <summary>
        /// Date is stored as string in the international format: YYYY-MM-DD
        /// </summary>
        public string Date { get; set; }
        public string Login { get; set; }
        public string Location { get; set; }
        public string Activity { get; set; }

    }
}
