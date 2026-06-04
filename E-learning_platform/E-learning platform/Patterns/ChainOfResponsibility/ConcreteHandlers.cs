namespace E_learning_platform.Patterns.ChainOfResponsibility;

/// <summary>
/// FAQ-ul automat — primul nivel. Tratează doar probleme banale de cont (severitate 1).
/// </summary>
public class FaqBotHandler : SupportHandler
{
    protected override bool CanHandle(SupportRequest request)
        => request.Type == SupportRequestType.AccountIssue && request.Severity <= 1;

    protected override void Process(SupportRequest request)
    {
        request.HandledBy = "FAQ Bot";
        request.Resolution = "Articol din baza de cunoștințe trimis automat utilizatorului.";
    }
}

/// <summary>
/// Operatorul de suport de nivel 1 — tratează probleme de cont și conținut, severitate ≤ 2.
/// </summary>
public class Level1SupportHandler : SupportHandler
{
    protected override bool CanHandle(SupportRequest request)
        => (request.Type == SupportRequestType.AccountIssue || request.Type == SupportRequestType.CourseContent)
           && request.Severity <= 2;

    protected override void Process(SupportRequest request)
    {
        request.HandledBy = "Suport Nivel 1";
        request.Resolution = $"Operatorul a răspuns întrebării utilizatorului {request.RequesterName}.";
    }
}

/// <summary>
/// Specialist plăți — tratează exclusiv probleme legate de facturare și abonamente.
/// </summary>
public class BillingSpecialistHandler : SupportHandler
{
    protected override bool CanHandle(SupportRequest request)
        => request.Type == SupportRequestType.PaymentIssue;

    protected override void Process(SupportRequest request)
    {
        request.HandledBy = "Specialist Facturare";
        request.Resolution = "Tranzacția a fost verificată și problema de plată rezolvată.";
    }
}

/// <summary>
/// Inginer tehnic — tratează bug-urile platformei.
/// </summary>
public class TechnicalEngineerHandler : SupportHandler
{
    protected override bool CanHandle(SupportRequest request)
        => request.Type == SupportRequestType.TechnicalBug;

    protected override void Process(SupportRequest request)
    {
        request.HandledBy = "Inginer Tehnic";
        request.Resolution = "Bug-ul a fost reprodus, ticket creat în Jira și planificat pentru sprint.";
    }
}

/// <summary>
/// Echipa de securitate — ultimul nivel, tratează incidente de securitate sau orice severitate maximă.
/// </summary>
public class SecurityTeamHandler : SupportHandler
{
    protected override bool CanHandle(SupportRequest request)
        => request.Type == SupportRequestType.SecurityIncident || request.Severity >= 5;

    protected override void Process(SupportRequest request)
    {
        request.HandledBy = "Echipa Securitate";
        request.Resolution = "Incident escaladat, conturile compromise au fost izolate, audit pornit.";
    }
}
