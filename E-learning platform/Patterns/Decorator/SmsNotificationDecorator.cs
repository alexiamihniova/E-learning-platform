using System;
using E_learning_platform.Interfaces;

namespace E_learning_platform.Patterns.Decorator
{
    public class SmsNotificationDecorator : NotificationDecorator
    {
        public SmsNotificationDecorator(INotificationService notifier) : base(notifier)
        {
        }

        public override void Notify(string to, string message)
        {
            base.Notify(to, message);
            SendSms(to, message);
        }

        private void SendSms(string to, string message)
        {
            Console.WriteLine($"Sending SMS to {to}: {message}");
        }
    }
}
