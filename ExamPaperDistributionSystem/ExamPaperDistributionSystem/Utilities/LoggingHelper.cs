using System;
using System.Diagnostics;

namespace ExamPaperDistributionSystem.Utilities
{
    public static class LoggingHelper
    {
        private static readonly string LogFilePath = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"];
        private static readonly string LogLevel = System.Configuration.ConfigurationManager.AppSettings["LogLevel"];

        private static void Log(string message, TraceEventType eventType)
        {
            using (var traceListener = new TextWriterTraceListener(LogFilePath))
            {
                var traceSource = new TraceSource("ApplicationSource");
                traceSource.Switch = new SourceSwitch("AppSwitch", LogLevel);
                traceSource.Listeners.Clear();
                traceSource.Listeners.Add(traceListener);
                traceSource.TraceEvent(eventType, 0, message);
                traceSource.Close();
            }
        }

        public static void Info(string message)
        {
            Log(message, TraceEventType.Information);
        }

        public static void Warning(string message)
        {
            Log(message, TraceEventType.Warning);
        }

        public static void Error(string message)
        {
            Log(message, TraceEventType.Error);
        }
    }
}
