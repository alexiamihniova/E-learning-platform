namespace E_learning_platform.Patterns.FactoryMethod
{
    // Product Interface
    // Why: Defines the common interface for all objects the factory can create.
    // This allows the client code to work with any lesson type without knowing its concrete class.
    public interface ILesson
    {
        void Open();
    }
}
