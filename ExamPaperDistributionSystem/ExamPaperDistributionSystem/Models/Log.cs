using ExamPaperDistributionSystem.Services;
using System;
using System.Web.Services.Description;

namespace ExamPaperDistributionSystem.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }

        public string Action { get; set; }
        public LogLevel LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
