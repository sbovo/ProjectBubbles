using ProjectBubbles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{

    public class Result
    {
        public Item[] result { get; set; }
        public int id { get; set; }
        public object exception { get; set; }
        public int status { get; set; }
        public bool isCanceled { get; set; }
        public bool isCompleted { get; set; }
        public bool isCompletedSuccessfully { get; set; }
        public int creationOptions { get; set; }
        public object asyncState { get; set; }
        public bool isFaulted { get; set; }
    }
}
