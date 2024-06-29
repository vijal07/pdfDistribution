using System;
using System.Collections.Generic;
using System.IO;
using ExamPaperDistributionSystem.Models;

namespace ExamPaperDistributionSystem.Services
{
    public class LogService
    {
        private readonly string _logFilePath = "";

        public LogService()
        {
        }

        public void Log(string message, LogLevel logLevel)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(_logFilePath))
                {
                    sw.WriteLine($"[{DateTime.Now}] [{logLevel}] - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging: {ex.Message}");
            }
        }

        public List<Log> GetAllLogs()
        {
            List<Log> logs = new List<Log>();

            try
            {
                using (StreamReader sr = new StreamReader(_logFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Assuming log format: [Timestamp] [LogLevel] - Message
                        string[] parts = line.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            string timestamp = parts[0].TrimStart('[').TrimEnd(']');
                            string logLevelString = parts[1].Substring(0, parts[1].IndexOf("]")).TrimStart('[').TrimEnd(']');
                            string message = parts[1].Substring(parts[1].IndexOf("]") + 1).Trim();

                            LogLevel logLevel;
                            Enum.TryParse(logLevelString, true, out logLevel);

                            Log log = new Log
                            {
                                Timestamp = DateTime.Parse(timestamp),
                                LogLevel = logLevel,
                                Message = message
                            };

                            logs.Add(log);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading logs: {ex.Message}");
            }

            return logs;
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
}
