﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProjectBubbles

{
    public class Settings
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string UserName { get; set; }
    }
}
