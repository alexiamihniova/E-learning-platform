using E_learning_platform.Interfaces;

namespace E_learning_platform.Patterns.Decorator
{
    public abstract class NotificationDecorator : INotificationService
    {
        protected readonly INotificationService _notifier;

        protected NotificationDecorator(INotificationService notifier)
        {
            _notifier = notifier;
        }

        public virtual void Notify(string to, string message)
        {
            _notifier.Notify(to, message);
        }
    }
}
