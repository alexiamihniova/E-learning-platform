namespace E_learning_platform.Patterns.Strategy
{
    public class PassFailGradingStrategy : IGradingStrategy
    {
        public string Grade(int score)
        {
            return score >= 50 ? "Pass" : "Fail";
        }
    }
}
