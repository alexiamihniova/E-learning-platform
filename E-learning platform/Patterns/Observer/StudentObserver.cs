using System;

namespace E_learning_platform.Patterns.Observer
{
    public class StudentObserver : ICourseObserver
    {
        public string Name { get; private set; }
        public string? LastNotification { get; private set; }

        public StudentObserver(string name)
        {
            Name = name;
        }

        public void Update(string courseName, string message)
        {
            LastNotification = $"[{Name}] received from {courseName}: {message}";
            Console.WriteLine(LastNotification);
        }
    }
}
