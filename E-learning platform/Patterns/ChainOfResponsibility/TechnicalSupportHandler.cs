namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public class TechnicalSupportHandler : BaseSupportHandler
    {
        public override string Handle(SupportTicket ticket)
        {
            if (ticket.Severity == SeverityLevel.Medium)
            {
                ticket.IsProcessed = true;
                ticket.ProcessedBy = "Technical Support Specialist";
                return "Tech Support: Ticket regarding '" + ticket.Subject + "' was resolved by a support specialist.";
            }
            return base.Handle(ticket);
        }
    }
}
