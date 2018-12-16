using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBubbles.Models
{
    public class Profile
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhotoBase64Encoded { get; set; }
    }
}