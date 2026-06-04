namespace E_learning_platform.Patterns.Visitor
{
    public class VideoLessonElement : ICourseElement
    {
        public string Title { get; }
        public int DurationMinutes { get; }

        public VideoLessonElement(string title, int durationMinutes)
        {
            Title = title;
            DurationMinutes = durationMinutes;
        }

        public void Accept(ICourseElementVisitor visitor)
        {
            visitor.VisitVideo(this);
        }
    }
}
