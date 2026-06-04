namespace E_learning_platform.Patterns.Observer
{
    public interface ICourseSubject
    {
        void Attach(ICourseObserver observer);
        void Detach(ICourseObserver observer);
        void Notify();
    }
}
