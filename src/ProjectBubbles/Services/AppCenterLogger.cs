using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;

namespace ProjectBubbles.Services
{
    public class AppCenterLogger : ILogger
    {
        public void Log(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }
    }
}
