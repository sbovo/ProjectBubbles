using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBubbles.Services
{
    public interface ILogger
    {
        void Log(string name, IDictionary<string, string> properties = null);
        void LogError(Exception ex, IDictionary<string, string> properties = null);
    }
}
