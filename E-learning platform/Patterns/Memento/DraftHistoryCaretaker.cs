using System.Collections.Generic;

namespace E_learning_platform.Patterns.Memento
{
    public class DraftHistoryCaretaker
    {
        private Stack<AssignmentDraftMemento> _history = new Stack<AssignmentDraftMemento>();
        private AssignmentDraftOriginator _originator;

        public DraftHistoryCaretaker(AssignmentDraftOriginator originator)
        {
            _originator = originator;
        }

        public void Backup()
        {
            _history.Push(_originator.SaveDraft());
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var memento = _history.Pop();
                _originator.RestoreDraft(memento);
            }
        }
        
        public int GetHistoryCount()
        {
            return _history.Count;
        }
    }
}
