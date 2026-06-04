namespace E_learning_platform.Patterns.Strategy
{
    public class StandardGradingStrategy : IGradingStrategy
    {
        public string Grade(int score)
        {
            if (score >= 90) return "A";
            if (score >= 80) return "B";
            if (score >= 70) return "C";
            if (score >= 60) return "D";
            return "F";
        }
    }
}
