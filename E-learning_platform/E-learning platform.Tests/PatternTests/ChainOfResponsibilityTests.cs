using E_learning_platform.Patterns.ChainOfResponsibility;
using Xunit;

namespace E_learning_platform.Tests.PatternTests;

public class ChainOfResponsibilityTests
{
    private static SupportHandler BuildChain()
    {
        var faq = new FaqBotHandler();
        var l1  = new Level1SupportHandler();
        var bil = new BillingSpecialistHandler();
        var tec = new TechnicalEngineerHandler();
        var sec = new SecurityTeamHandler();
        faq.SetNext(l1).SetNext(bil).SetNext(tec).SetNext(sec);
        return faq;
    }

    [Fact]
    public void FaqBot_Handles_TrivialAccountIssue()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Alexia", SupportRequestType.AccountIssue, "Reset parola", 1);
        chain.Handle(req);
        Assert.Equal("FAQ Bot", req.HandledBy);
    }

    [Fact]
    public void Level1_Handles_CourseContentQuestion()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Andrei", SupportRequestType.CourseContent, "Întrebare lecție", 2);
        chain.Handle(req);
        Assert.Equal("Suport Nivel 1", req.HandledBy);
    }

    [Fact]
    public void Billing_Handles_PaymentIssues()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Maria", SupportRequestType.PaymentIssue, "Dublă debitare", 3);
        chain.Handle(req);
        Assert.Equal("Specialist Facturare", req.HandledBy);
    }

    [Fact]
    public void Engineer_Handles_TechnicalBugs()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Ion", SupportRequestType.TechnicalBug, "Eroare 500", 4);
        chain.Handle(req);
        Assert.Equal("Inginer Tehnic", req.HandledBy);
    }

    [Fact]
    public void Security_Handles_SecurityIncidents()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Diana", SupportRequestType.SecurityIncident, "Cont compromis", 5);
        chain.Handle(req);
        Assert.Equal("Echipa Securitate", req.HandledBy);
    }

    [Fact]
    public void Security_Handles_AnyMaxSeverity_Request()
    {
        var chain = BuildChain();
        // Severity 5 dar fără tip specific de securitate — totuși ar trebui să ajungă la securitate
        var req = new SupportRequest("X", SupportRequestType.AccountIssue, "Activitate suspectă", 5);
        chain.Handle(req);
        Assert.Equal("Echipa Securitate", req.HandledBy);
    }

    [Fact]
    public void Unhandled_Request_Returns_EscalationMessage()
    {
        // Lanț fără handlerul de securitate
        var faq = new FaqBotHandler();
        var l1  = new Level1SupportHandler();
        faq.SetNext(l1);

        var req = new SupportRequest("Y", SupportRequestType.PaymentIssue, "...", 3);
        faq.Handle(req);

        Assert.Null(req.HandledBy);
        Assert.Contains("escaladare manuală", req.Resolution);
    }

    [Fact]
    public void Chain_Order_Matters_FaqHasPriority_Over_Level1()
    {
        var chain = BuildChain();
        var req = new SupportRequest("Z", SupportRequestType.AccountIssue, "Reset", 1);
        chain.Handle(req);
        // FAQ vine primul în lanț pentru severitate 1
        Assert.Equal("FAQ Bot", req.HandledBy);
    }
}
