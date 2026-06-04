namespace E_learning_platform.Patterns.Iterator
{
    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }
}
