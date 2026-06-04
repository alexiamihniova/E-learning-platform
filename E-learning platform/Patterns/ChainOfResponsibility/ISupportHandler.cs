namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public interface ISupportHandler
    {
        ISupportHandler SetNext(ISupportHandler next);
        string Handle(SupportTicket ticket);
    }
}
