using System.Collections.Generic;

namespace E_learning_platform.Patterns.Command
{
    public class CommandInvoker
    {
        private Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var command = _commandHistory.Pop();
                command.Undo();
            }
        }
        
        public int GetHistoryCount()
        {
            return _commandHistory.Count;
        }
    }
}
