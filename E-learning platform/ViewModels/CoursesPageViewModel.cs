namespace E_learning_platform.ViewModels
{
    public class CoursesPageViewModel
    {
        public List<CourseCardViewModel> Courses { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public string SelectedCategory { get; set; } = "All";
        public string SearchQuery { get; set; } = string.Empty;
        public int TotalCount => Courses.Count;
    }
}
