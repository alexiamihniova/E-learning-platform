namespace E_learning_platform.Patterns.Strategy
{
    public class StudentAssignment
    {
        public string Title { get; set; }
        public int Score { get; set; }
        private IGradingStrategy _gradingStrategy;

        public StudentAssignment(string title, int score, IGradingStrategy gradingStrategy)
        {
            Title = title;
            Score = score;
            _gradingStrategy = gradingStrategy;
        }

        public void SetGradingStrategy(IGradingStrategy gradingStrategy)
        {
            _gradingStrategy = gradingStrategy;
        }

        public string GetGrade()
        {
            return _gradingStrategy.Grade(Score);
        }
    }
}
