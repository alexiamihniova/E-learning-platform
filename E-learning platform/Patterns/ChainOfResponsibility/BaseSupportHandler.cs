namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public abstract class BaseSupportHandler : ISupportHandler
    {
        private ISupportHandler? _nextHandler;

        public ISupportHandler SetNext(ISupportHandler next)
        {
            _nextHandler = next;
            return next;
        }

        public virtual string Handle(SupportTicket ticket)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(ticket);
            }
            return "All handlers failed to process the ticket.";
        }
    }
}
