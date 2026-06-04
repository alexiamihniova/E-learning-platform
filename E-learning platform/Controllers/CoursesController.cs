using E_learning_platform.ViewModels;
using E_learning_platform.Patterns.Strategy;
using E_learning_platform.Patterns.Observer;
using E_learning_platform.Patterns.Command;
using E_learning_platform.Patterns.Memento;
using E_learning_platform.Patterns.Iterator;
using Microsoft.AspNetCore.Mvc;

namespace E_learning_platform.Controllers
{
    public class CoursesController : Controller
    {
        private static List<CourseCardViewModel> GetAllCourses() => new()
        {
            new() { Id = 1, Title = "Advanced C# Design Patterns", Description = "Master all 23 GoF patterns: Creational, Structural and Behavioral.", Category = "Programming", CategoryColor = "primary", Price = 89.99m, Rating = 4.9, ReviewCount = 2100, InstructorName = "Dr. Maria Ionescu", LessonCount = 42, DurationHours = 18, Level = "Advanced", BadgeLabel = "BESTSELLER", ImageGradient = "#0062ff33", Tags = new() { "C#", ".NET", "Design Patterns" } },
            new() { Id = 2, Title = "Premium UI/UX Masterclass", Description = "Glassmorphism, Neumorphism and premium design techniques.", Category = "Design", CategoryColor = "success", Price = 74.99m, Rating = 4.8, ReviewCount = 1540, InstructorName = "Alex Popescu", LessonCount = 36, DurationHours = 14, Level = "Intermediate", BadgeLabel = "TOP RATED", ImageGradient = "#6c5ce733", Tags = new() { "Figma", "CSS", "UI Design" } },
            new() { Id = 3, Title = "Social Media Growth 2026", Description = "Build a brand in the modern digital landscape.", Category = "Marketing", CategoryColor = "warning", Price = 49.99m, Rating = 4.7, ReviewCount = 980, InstructorName = "Ioana Marin", LessonCount = 28, DurationHours = 10, Level = "Beginner", BadgeLabel = "", ImageGradient = "#00d2ff22", Tags = new() { "TikTok", "Instagram", "SEO" } },
            new() { Id = 4, Title = "ASP.NET Core 10 from Scratch", Description = "Build enterprise-grade web applications with C# and MVC.", Category = "Programming", CategoryColor = "primary", Price = 99.99m, Rating = 4.9, ReviewCount = 3200, InstructorName = "Dr. Maria Ionescu", LessonCount = 58, DurationHours = 24, Level = "Intermediate", BadgeLabel = "NEW", ImageGradient = "#0062ff44", Tags = new() { "ASP.NET", "MVC", "REST API" } },
            new() { Id = 5, Title = "Python for Data Science", Description = "From NumPy basics to Machine Learning pipelines.", Category = "Programming", CategoryColor = "primary", Price = 79.99m, Rating = 4.6, ReviewCount = 1200, InstructorName = "Andrei Stanciu", LessonCount = 45, DurationHours = 20, Level = "Beginner", BadgeLabel = "", ImageGradient = "#0062ff22", Tags = new() { "Python", "Pandas", "ML" } },
            new() { Id = 6, Title = "Mobile Dev with Flutter", Description = "Ship native iOS and Android apps with a single codebase.", Category = "Programming", CategoryColor = "primary", Price = 84.99m, Rating = 4.8, ReviewCount = 890, InstructorName = "Cristina Dumitrescu", LessonCount = 40, DurationHours = 16, Level = "Intermediate", BadgeLabel = "TRENDING", ImageGradient = "#00d2ff33", Tags = new() { "Flutter", "Dart", "Mobile" } },
        };

        public IActionResult Index(string category = "All", string search = "")
        {
            var all = GetAllCourses();
            var categories = all.Select(c => c.Category).Distinct().OrderBy(x => x).ToList();
            categories.Insert(0, "All");

            var filtered = all;
            if (category != "All")
                filtered = filtered.Where(c => c.Category == category).ToList();
            if (!string.IsNullOrWhiteSpace(search))
                filtered = filtered.Where(c => c.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                               c.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            var vm = new CoursesPageViewModel
            {
                Courses = filtered,
                Categories = categories,
                SelectedCategory = category,
                SearchQuery = search
            };
            return View(vm);
        }

        public IActionResult Detail(int id)
        {
            var course = GetAllCourses().FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();

            // Demonstrate Iterator Pattern
            var collection = new CourseModuleCollection();
            for (int i = 1; i <= course.LessonCount; i++)
            {
                collection.AddModule(new CourseModule(i, $"Module {i}: {GetModuleName(i, course.Category)}"));
            }

            ViewBag.CourseModules = new List<string>();
            var iterator = collection.CreateIterator();
            var moduleNames = new List<string>();
            while (iterator.HasNext())
            {
                var m = iterator.Next();
                if (m != null) moduleNames.Add(m.Title);
            }
            ViewBag.CourseModules = moduleNames.Take(8).ToList();
            ViewBag.TotalModules = course.LessonCount;

            return View(course);
        }

        private string GetModuleName(int index, string category) => (category, index % 6) switch
        {
            ("Programming", 1) => "Introduction & Setup",
            ("Programming", 2) => "Core Concepts",
            ("Programming", 3) => "Advanced Topics",
            ("Programming", 4) => "Project Building",
            ("Programming", 5) => "Testing & Debugging",
            ("Programming", 0) => "Deployment & Best Practices",
            ("Design", 1) => "Design Thinking",
            ("Design", 2) => "Tools & Workflow",
            ("Design", 3) => "Typography & Color",
            ("Design", 4) => "Component Systems",
            ("Design", 5) => "Prototyping",
            ("Design", 0) => "Portfolio Project",
            _ => "Lesson Content"
        };
    }
}
