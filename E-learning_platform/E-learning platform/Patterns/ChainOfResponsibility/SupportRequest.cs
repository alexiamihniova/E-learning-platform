namespace E_learning_platform.Patterns.ChainOfResponsibility;

/// <summary>
/// Tipurile de cereri de suport pe care le pot trimite utilizatorii platformei e-learning.
/// </summary>
public enum SupportRequestType
{
    AccountIssue,       // resetare parolă, login etc. — nivel începător
    CourseContent,      // întrebări legate de materiale, lecții
    PaymentIssue,       // facturare, abonamente
    TechnicalBug,       // erori platformă, probleme grave
    SecurityIncident    // incidente de securitate — nivel maxim
}

/// <summary>
/// Reprezintă o cerere de suport trimisă de un student/profesor pe platforma e-learning.
/// </summary>
public class SupportRequest
{
    public string RequesterName { get; }
    public SupportRequestType Type { get; }
    public string Description { get; }
    public int Severity { get; }   // 1 = minor, 5 = critic

    public string? HandledBy { get; set; }
    public string? Resolution { get; set; }

    public SupportRequest(string requesterName, SupportRequestType type, string description, int severity)
    {
        RequesterName = requesterName;
        Type = type;
        Description = description;
        Severity = severity;
    }

    public bool IsResolved => HandledBy is not null;
}
