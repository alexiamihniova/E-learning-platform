namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public class CriticalIncidentHandler : BaseSupportHandler
    {
        public override string Handle(SupportTicket ticket)
        {
            if (ticket.Severity == SeverityLevel.Critical)
            {
                ticket.IsProcessed = true;
                ticket.ProcessedBy = "Incident Response Team";
                return "Critical Response: Ticket regarding '" + ticket.Subject + "' was escalated to the CRT and resolved.";
            }
            return base.Handle(ticket);
        }
    }
}
