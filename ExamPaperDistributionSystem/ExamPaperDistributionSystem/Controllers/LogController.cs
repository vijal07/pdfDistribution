using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Services;

namespace ExamPaperDistributionSystem.Controllers
{
    public class LogController : Controller
    {
        private readonly LogService _logService;

        public LogController()
        {
            _logService = new LogService();
        }

        // GET: Log
        public ActionResult Index()
        {
            // Retrieve all logs from the service
            List<Log> logs = _logService.GetAllLogs();
            return View(logs);
        }

    }
}
