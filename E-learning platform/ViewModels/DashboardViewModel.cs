namespace E_learning_platform.ViewModels
{
    public class EnrolledCourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public int ProgressPercent { get; set; }
        public string Category { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = "primary";
        public string LastAccessed { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
    }

    public class DashboardViewModel
    {
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public int TotalEnrolled { get; set; }
        public int Completed { get; set; }
        public int InProgress { get; set; }
        public int TotalHoursLearned { get; set; }
        public List<EnrolledCourseViewModel> EnrolledCourses { get; set; } = new();
        public List<string> Notifications { get; set; } = new();
        public string CurrentAssignmentDraft { get; set; } = string.Empty;
        public List<string> DraftHistory { get; set; } = new();
    }
}
