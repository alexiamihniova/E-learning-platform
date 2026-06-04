using E_learning_platform.Interfaces;
using E_learning_platform.Models;

namespace E_learning_platform.Services
{
    public class EnrollmentManager
    {
        private readonly INotificationService _notificationService;

        public EnrollmentManager(INotificationService notificationService)
        {
            // Application of DIP: depends on interface, not concrete implementation
            _notificationService = notificationService;
        }

        public void Enroll(Student student, Course course)
        {
            var enrollment = new Enrollment(student, course);
            // In a real app, we would save enrollment to DB here.
            
            _notificationService.Notify(student.Email, $"You have successfully enrolled in {course.Title}. The price was {course.GetPrice():C}.");
        }
    }
}
