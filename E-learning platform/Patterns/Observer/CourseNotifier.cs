using System.Collections.Generic;

namespace E_learning_platform.Patterns.Observer
{
    public class CourseNotifier : ICourseSubject
    {
        private List<ICourseObserver> _observers = new List<ICourseObserver>();
        private string _courseName;
        private string? _lastUpdateMessage;

        public CourseNotifier(string courseName)
        {
            _courseName = courseName;
        }

        public void Attach(ICourseObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(ICourseObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_courseName, _lastUpdateMessage);
            }
        }

        public void AddNewMaterial(string materialName)
        {
            _lastUpdateMessage = $"New material added: {materialName}";
            Notify();
        }
    }
}
