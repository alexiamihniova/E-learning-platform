using E_learning_platform.Patterns.Strategy;
using E_learning_platform.Patterns.Command;
using Microsoft.AspNetCore.Mvc;

namespace E_learning_platform.Controllers
{
    public class PatternsController : Controller
    {
        private static readonly CourseManagerReceiver _enrollReceiver = new();
        private static readonly CommandInvoker _invoker = new();
        private static readonly List<string> _enrollLog = new();

        public IActionResult Index()
        {
            // Strategy demo data
            var scores = new[] { 92, 78, 55, 40, 83 };
            var standardStrategy = new StandardGradingStrategy();
            var passFailStrategy = new PassFailGradingStrategy();

            ViewBag.StrategyResults = scores.Select(s => new
            {
                Score = s,
                Standard = standardStrategy.Grade(s),
                PassFail = passFailStrategy.Grade(s)
            }).ToList();

            ViewBag.EnrollLog = new List<string>(_enrollLog);
            ViewBag.CanUndo = _invoker.GetHistoryCount() > 0;
            return View();
        }

        [HttpPost]
        public IActionResult Enroll(string studentName, string courseName)
        {
            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(courseName))
                return RedirectToAction("Index");

            var cmd = new EnrollCommand(_enrollReceiver, studentName, courseName);
            _invoker.ExecuteCommand(cmd);
            _enrollLog.Add($"✅ Enrolled: {studentName} → {courseName}");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UndoEnroll()
        {
            if (_invoker.GetHistoryCount() > 0)
            {
                _invoker.UndoLastCommand();
                _enrollLog.Add($"↩️ Undo: last enrollment reversed.");
            }
            return RedirectToAction("Index");
        }
    }
}
