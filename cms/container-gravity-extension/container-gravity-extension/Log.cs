using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager;
using Tridion.Logging;

namespace SDL.DXA.Extensions.Container
{
    /// <summary>
    /// Simple log abstraction around the Tridion logging.
    /// </summary>
    class Log
    {
        static public void Info(string message)
        {
            LogMessage(message, System.Diagnostics.TraceEventType.Information);
        }

        static public void Warn(string message)
        {
            LogMessage(message, System.Diagnostics.TraceEventType.Warning);
        }

        static public void Error(string message)
        {
            LogMessage(message, System.Diagnostics.TraceEventType.Error);
        }

        static protected void LogMessage(string message, System.Diagnostics.TraceEventType eventType)
        {
            Logger.Write(message, "ContainerGravityHandler", LogCategory.Custom, eventType);
        }
    }
}
