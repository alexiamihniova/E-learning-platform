namespace E_learning_platform.Patterns.Prototype
{
    public interface IPrototype<T>
    {
        T Clone();
        T DeepClone();
    }
}
