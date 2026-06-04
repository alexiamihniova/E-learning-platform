namespace E_learning_platform.Patterns.Iterator
{
    public interface IIterator<T>
    {
        bool HasNext();
        T? Next();
    }
}
