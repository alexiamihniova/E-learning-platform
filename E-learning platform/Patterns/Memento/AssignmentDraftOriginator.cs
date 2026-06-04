using System;

namespace E_learning_platform.Patterns.Memento
{
    public class AssignmentDraftOriginator
    {
        public string? Content { get; set; }

        public AssignmentDraftMemento SaveDraft()
        {
            Console.WriteLine("Saving draft...");
            return new AssignmentDraftMemento(Content);
        }

        public void RestoreDraft(AssignmentDraftMemento memento)
        {
            if (memento != null)
            {
                Content = memento.Content;
                Console.WriteLine("Draft restored.");
            }
        }
    }
}
