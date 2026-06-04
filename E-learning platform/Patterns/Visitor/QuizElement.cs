namespace E_learning_platform.Patterns.Visitor
{
    public class QuizElement : ICourseElement
    {
        public string Title { get; }
        public int QuestionCount { get; }

        public QuizElement(string title, int questionCount)
        {
            Title = title;
            QuestionCount = questionCount;
        }

        public void Accept(ICourseElementVisitor visitor)
        {
            visitor.VisitQuiz(this);
        }
    }
}
