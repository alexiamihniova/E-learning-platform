using E_learning_platform.Models;

namespace E_learning_platform.Patterns.Builder
{
    public interface ICourseBuilder
    {
        void Reset();
        void SetTitle(string title);
        void SetPrice(decimal price);
        void AddModule(string module);
        Course GetCourse();
    }
}
