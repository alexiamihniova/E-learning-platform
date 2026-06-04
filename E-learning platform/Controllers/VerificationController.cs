using Microsoft.AspNetCore.Mvc;
using E_learning_platform.Models;
using E_learning_platform.Services;
using E_learning_platform.Interfaces;
using System.Text;

namespace E_learning_platform.Controllers
{
    public class VerificationController : Controller
    {
        public IActionResult Index()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== SOLID Lab 1 Verification ===");

            // 1. Create Users
            var student = new Student(1, "DeepMind Student", "student@example.com");
            var teacher = new Teacher(2, "Professor AI", "prof@example.com");

            sb.AppendLine($"Created Student: {student.Name}");
            sb.AppendLine($"Created Teacher: {teacher.Name}");

            // Polymorphism Verification
            var users = new List<User> { student, teacher };
            foreach (var user in users)
            {
                sb.AppendLine($"User: {user.Name}, Role: {user.GetRole()}");
            }

            // 2. ISP Check
            student.Subscribe(); 
            // student.Grade(); // Compilation error if tried
            teacher.Grade();
            // teacher.Subscribe(); // Compilation error if tried
            sb.AppendLine("ISP Verified: Student can Subscribe, Teacher can Grade.");

            // 3. Strategy & OCP Check
            var course = new Course(101, "Advanced AI", 100m, new StandardPriceStrategy());
            sb.AppendLine($"Course: {course.Title}, Base Price: {course.BasePrice}, Standard Price: {course.GetPrice()}");

            course.SetPriceStrategy(new DiscountPriceStrategy(0.2m)); // 20% off
            sb.AppendLine($"Course Price with 20% DiscountStrategy: {course.GetPrice()}");

            // 4. DIP & Manager Check
            // We inject EmailService (User Notification)
            INotificationService emailService = new EmailService(); 
            var enrollmentManager = new EnrollmentManager(emailService);

            enrollmentManager.Enroll(student, course);
            sb.AppendLine($"Enrolled {student.Name} in {course.Title}. Email notification simulated (check console/logs).");

            return Content(sb.ToString(), "text/plain");
        }
    }
}
