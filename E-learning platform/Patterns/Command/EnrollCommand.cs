namespace E_learning_platform.Patterns.Command
{
    public class EnrollCommand : ICommand
    {
        private CourseManagerReceiver _receiver;
        private string _studentName;
        private string _courseName;

        public EnrollCommand(CourseManagerReceiver receiver, string studentName, string courseName)
        {
            _receiver = receiver;
            _studentName = studentName;
            _courseName = courseName;
        }

        public void Execute()
        {
            _receiver.EnrollStudent(_studentName, _courseName);
        }

        public void Undo()
        {
            _receiver.DropStudent(_studentName, _courseName);
        }
    }
}
