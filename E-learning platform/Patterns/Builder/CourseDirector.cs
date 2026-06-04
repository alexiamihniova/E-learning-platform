using E_learning_platform.Patterns.Builder;

namespace E_learning_platform.Patterns.Builder
{
    public class CourseDirector
    {
        private readonly ICourseBuilder _builder;

        public CourseDirector(ICourseBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructBasicCourse()
        {
            _builder.Reset();
            _builder.SetTitle("Basic C# Course");
            _builder.SetPrice(50.0m);
            _builder.AddModule("Introduction");
            _builder.AddModule("Variables and Types");
            _builder.AddModule("Control Flow");
        }

        public void ConstructPremiumCourse()
        {
            _builder.Reset();
            _builder.SetTitle("Premium C# Masterclass");
            _builder.SetPrice(150.0m);
            _builder.AddModule("Introduction");
            _builder.AddModule("OOP Principles");
            _builder.AddModule("Design Patterns");
            _builder.AddModule("Asynchronous Programming");
            _builder.AddModule("Final Project");
        }
    }
}
