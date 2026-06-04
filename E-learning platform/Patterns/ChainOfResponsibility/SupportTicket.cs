namespace E_learning_platform.Patterns.ChainOfResponsibility
{
    public enum SeverityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    public class SupportTicket
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SeverityLevel Severity { get; set; }
        public bool IsProcessed { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;
    }
}
