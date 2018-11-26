using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBubbles.Helpers
{
    internal class LogHelper
    {
        internal static void Log(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }
    }
}
