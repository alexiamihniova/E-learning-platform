using E_learning_platform.Interfaces;
using E_learning_platform.Models;
using E_learning_platform.Services;
using System;

namespace E_learning_platform.Patterns.Facade
{
    /// <summary>
    /// Facade Class: Provides a simple, unified interface to a complex subsystem
    /// (Payment Processing + Enrollment Management + Notifications).
    /// </summary>
    public class CourseEnrollmentFacade
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly EnrollmentManager _enrollmentManager;

        public CourseEnrollmentFacade(IPaymentProcessor paymentProcessor, EnrollmentManager enrollmentManager)
        {
            _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
            _enrollmentManager = enrollmentManager ?? throw new ArgumentNullException(nameof(enrollmentManager));
        }

        /// <summary>
        /// A single, simplified method that orchestrates multiple subsystems.
        /// </summary>
        public bool BuyCourse(Student student, Course course)
        {
            Console.WriteLine($"\n[Facade] Starting enrollment process for student ID {student.Id} in course '{course.Title}'...");

            // 1. Process Payment via the Adapter
            decimal price = course.GetPrice();
            bool paymentSuccess = _paymentProcessor.ProcessPayment(price);

            if (!paymentSuccess)
            {
                Console.WriteLine("[Facade] Payment failed. Enrollment aborted.");
                return false;
            }

            // 2. Enroll Student (EnrollmentManager also handles notifications internally)
            _enrollmentManager.Enroll(student, course);

            Console.WriteLine("[Facade] Enrollment process completed successfully.");
            return true;
        }
    }
}
