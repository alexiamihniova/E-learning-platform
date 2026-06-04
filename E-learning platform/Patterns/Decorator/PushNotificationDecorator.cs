using System;
using E_learning_platform.Interfaces;

namespace E_learning_platform.Patterns.Decorator
{
    public class PushNotificationDecorator : NotificationDecorator
    {
        public PushNotificationDecorator(INotificationService notifier) : base(notifier)
        {
        }

        public override void Notify(string to, string message)
        {
            base.Notify(to, message);
            SendPushNotification(to, message);
        }

        private void SendPushNotification(string to, string message)
        {
            Console.WriteLine($"Sending Push Notification to {to}: {message}");
        }
    }
}
