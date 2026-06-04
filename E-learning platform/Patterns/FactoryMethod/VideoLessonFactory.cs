namespace E_learning_platform.Patterns.FactoryMethod
{
    public class VideoLessonFactory : LessonFactory
    {
        // REFACTORING 2: Expression-bodied member
        public override ILesson CreateLesson(string title) => new VideoLesson(title);
    }
}
