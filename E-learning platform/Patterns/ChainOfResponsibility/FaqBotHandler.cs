namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public class FaqBotHandler : BaseSupportHandler
    {
        public override string Handle(SupportTicket ticket)
        {
            if (ticket.Severity == SeverityLevel.Low)
            {
                ticket.IsProcessed = true;
                ticket.ProcessedBy = "FAQ Bot (AI)";
                return "FAQ Bot: Ticket regarding '" + ticket.Subject + "' was resolved automatically with documentation link.";
            }
            return base.Handle(ticket);
        }
    }
}
