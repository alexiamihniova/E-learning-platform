namespace E_learning_platform.ViewModels
{
    public class CourseCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = "primary";
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public int LessonCount { get; set; }
        public int DurationHours { get; set; }
        public string Level { get; set; } = "Beginner";
        public string BadgeLabel { get; set; } = string.Empty;
        public string ImageGradient { get; set; } = "#0062ff22";
        public List<string> Tags { get; set; } = new();
    }
}
