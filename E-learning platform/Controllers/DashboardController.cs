using E_learning_platform.ViewModels;
using E_learning_platform.Patterns.Strategy;
using E_learning_platform.Patterns.Observer;
using E_learning_platform.Patterns.Command;
using E_learning_platform.Patterns.Memento;
using Microsoft.AspNetCore.Mvc;

namespace E_learning_platform.Controllers
{
    public class DashboardController : Controller
    {
        // In a real app this would come from a database; we use in-memory state for demo
        private static readonly AssignmentDraftOriginator _draftOriginator = new() { Content = "My initial assignment draft..." };
        private static readonly DraftHistoryCaretaker _caretaker = new DraftHistoryCaretaker(_draftOriginator);
        private static readonly List<string> _draftVersions = new() { "My initial assignment draft..." };

        private static readonly CourseNotifier _courseNotifier = new("Advanced C# Design Patterns");
        private static readonly List<string> _notifications = new();

        static DashboardController()
        {
            var student = new StudentObserver("Ion Popescu");
            var student2 = new StudentObserver("Maria Ionescu");
            _courseNotifier.Attach(new DashboardStudentObserver(_notifications, "Ion Popescu"));
            _courseNotifier.AddNewMaterial("Lab 6 – Behavioral Patterns");
            _courseNotifier.AddNewMaterial("Quiz: Strategy vs Command");
        }

        private DashboardViewModel BuildViewModel()
        {
            return new DashboardViewModel
            {
                StudentName = "Ion Popescu",
                StudentEmail = "ion.popescu@elearn.pro",
                TotalEnrolled = 4,
                Completed = 1,
                InProgress = 3,
                TotalHoursLearned = 47,
                EnrolledCourses = new()
                {
                    new() { CourseId = 1, Title = "Advanced C# Design Patterns", InstructorName = "Dr. Maria Ionescu", ProgressPercent = 72, Category = "Programming", CategoryColor = "primary", LastAccessed = "Today", Grade = "B+" },
                    new() { CourseId = 2, Title = "Premium UI/UX Masterclass", InstructorName = "Alex Popescu", ProgressPercent = 45, Category = "Design", CategoryColor = "success", LastAccessed = "Yesterday", Grade = "-" },
                    new() { CourseId = 4, Title = "ASP.NET Core 10 from Scratch", InstructorName = "Dr. Maria Ionescu", ProgressPercent = 20, Category = "Programming", CategoryColor = "primary", LastAccessed = "3 days ago", Grade = "-" },
                    new() { CourseId = 3, Title = "Social Media Growth 2026", InstructorName = "Ioana Marin", ProgressPercent = 100, Category = "Marketing", CategoryColor = "warning", LastAccessed = "Last week", Grade = "A" },
                },
                Notifications = _notifications.TakeLast(5).ToList(),
                CurrentAssignmentDraft = _draftOriginator.Content,
                DraftHistory = new(_draftVersions.TakeLast(4).Reverse()),
            };
        }

        public IActionResult Index()
        {
            return View(BuildViewModel());
        }

        [HttpPost]
        public IActionResult SaveDraft(string content)
        {
            _caretaker.Backup();
            _draftVersions.Add(_draftOriginator.Content);
            _draftOriginator.Content = content;
            TempData["DraftMessage"] = "Draft saved successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UndoDraft()
        {
            _caretaker.Undo();
            if (_draftVersions.Count > 1) _draftVersions.RemoveAt(_draftVersions.Count - 1);
            TempData["DraftMessage"] = "Draft restored to previous version.";
            return RedirectToAction("Index");
        }
    }

    // adapter for notifications list
    public class DashboardStudentObserver : E_learning_platform.Patterns.Observer.ICourseObserver
    {
        private readonly List<string> _log;
        private readonly string _name;
        public DashboardStudentObserver(List<string> log, string name) { _log = log; _name = name; }
        public void Update(string courseName, string message)
        {
            _log.Add($"📣 [{courseName}] {message}");
        }
    }
}
