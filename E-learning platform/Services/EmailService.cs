using E_learning_platform.Interfaces;
using System;

namespace E_learning_platform.Services
{
    public class EmailService : INotificationService
    {
        public void Notify(string to, string message)
        {
            // Simulate sending email
            Console.WriteLine($"[EmailService] Sending email to {to}: {message}");
        }
    }
}
