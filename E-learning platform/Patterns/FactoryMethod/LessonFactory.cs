namespace E_learning_platform.Patterns.FactoryMethod
{
    // Creator (Abstract)
    // Why: Declares the factory method representing the creation logic.
    // It doesn't know the actual concrete classes, decoupling the creation process.
    public abstract class LessonFactory
    {
        // The Factory Method
        public abstract ILesson CreateLesson(string title);

        // Optional: Shared logic can live here
        public void LogCreation(string title)
        {
            System.Console.WriteLine($"Log: Creating a new lesson titled '{title}'");
        }
    }
}
