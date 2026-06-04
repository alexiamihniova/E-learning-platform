namespace E_learning_platform.Patterns.Iterator
{
    public class CourseModule
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public CourseModule(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
