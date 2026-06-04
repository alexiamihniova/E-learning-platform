namespace E_learning_platform.Patterns.FactoryMethod
{
    public class TextLessonFactory : LessonFactory
    {
        // REFACTORING 2: Expression-bodied member
        public override ILesson CreateLesson(string title)
        {
            // Providing default content since Factory Method signature only asks for title.
            // In a real app, we might need to extend the factory or use metadata.
            return new TextLesson(title, "Default content for " + title);
        }
    }
}
