namespace E_learning_platform.Patterns.TemplateMethod
{
    public class CourseProgressReport : ReportGenerator
    {
        private readonly string _studentName;
        private readonly int _progress;

        public CourseProgressReport(string studentName, int progressPercentage)
        {
            _studentName = studentName;
            _progress = progressPercentage;
        }

        protected override string PrintHeader()
        {
            return "========== COURSE PROGRESS REPORT ==========\n" +
                   "Student: " + _studentName;
        }

        protected override string PrintContent()
        {
            return "Current Completion: " + _progress + "%\n" +
                   "Status: " + (_progress >= 100 ? "COMPLETED" : "IN PROGRESS");
        }
    }
}
