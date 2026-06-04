namespace E_learning_platform.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
