namespace E_learning_platform.Patterns.Observer
{
    public interface ICourseObserver
    {
        void Update(string courseName, string message);
    }
}
