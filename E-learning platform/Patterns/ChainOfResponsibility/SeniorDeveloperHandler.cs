namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public class SeniorDeveloperHandler : BaseSupportHandler
    {
        public override string Handle(SupportTicket ticket)
        {
            if (ticket.Severity == SeverityLevel.High)
            {
                ticket.IsProcessed = true;
                ticket.ProcessedBy = "Senior Software Engineer";
                return "Senior Dev: Ticket regarding '" + ticket.Subject + "' required a code fix and was resolved.";
            }
            return base.Handle(ticket);
        }
    }
}
