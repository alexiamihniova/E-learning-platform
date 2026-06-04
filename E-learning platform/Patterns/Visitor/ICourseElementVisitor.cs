namespace E_learning_platform.Patterns.Visitor
{
    public interface ICourseElementVisitor
    {
        void VisitVideo(VideoLessonElement element);
        void VisitQuiz(QuizElement element);
        void VisitAssignment(AssignmentElement element);
        string GetResult();
    }
}
