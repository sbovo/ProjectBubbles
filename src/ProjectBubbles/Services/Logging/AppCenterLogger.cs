using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace ProjectBubbles.Services
{
    public class AppCenterLogger : ILogger
    {
        public AppCenterLogger()
        {
            AppCenter.Start("android=bbaa52cb-718e-4c6f-91b2-ceee96cce5be;"
                + "uwp=bc566608-1fd9-4dc5-91fe-8b532cd1cd16;"
                + "ios=59ec4157-e51c-4c2a-bc94-f093558c6605",
                typeof(Analytics), typeof(Crashes));
        }
        public void Log(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }

        public void LogError(Exception ex, IDictionary<string, string> properties = null)
        {
            Crashes.TrackError(ex, properties);
        }
    }
}
