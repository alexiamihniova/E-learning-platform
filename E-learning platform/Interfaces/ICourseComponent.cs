namespace E_learning_platform.Interfaces
{
    /// <summary>
    /// Component interface for the Composite pattern.
    /// Represents both individual objects (Courses) and collections (CourseCategories).
    /// </summary>
    public interface ICourseComponent
    {
        string Title { get; }
        decimal GetPrice();
        void Display(int depth);
        void Add(ICourseComponent component);
        void Remove(ICourseComponent component);
    }
}
