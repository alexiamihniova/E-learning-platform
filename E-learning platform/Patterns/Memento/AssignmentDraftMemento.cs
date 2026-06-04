namespace E_learning_platform.Patterns.Memento
{
    public class AssignmentDraftMemento
    {
        public string Content { get; private set; }

        public AssignmentDraftMemento(string content)
        {
            Content = content;
        }
    }
}
