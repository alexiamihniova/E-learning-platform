using E_learning_platform.Patterns.State;
using Xunit;

namespace E_learning_platform.Tests.PatternTests;

public class StateTests
{
    [Fact]
    public void NewEnrollment_Starts_In_Draft()
    {
        var e = new CourseEnrollment("Alexia", "DIS");
        Assert.Equal("Draft", e.CurrentStateName);
    }

    [Fact]
    public void HappyPath_GoesThrough_All_States_To_Completed()
    {
        var e = new CourseEnrollment("Alexia", "DIS");
        e.Submit();
        Assert.Equal("PendingPayment", e.CurrentStateName);
        e.Pay();
        Assert.Equal("Active", e.CurrentStateName);
        e.Start();
        Assert.Equal("InProgress", e.CurrentStateName);
        e.Progress = 100;
        e.Complete();
        Assert.Equal("Completed", e.CurrentStateName);
    }

    [Fact]
    public void Cancel_Is_Allowed_From_Any_NonTerminal_State()
    {
        var e1 = new CourseEnrollment("A", "C");
        e1.Cancel();
        Assert.Equal("Cancelled", e1.CurrentStateName);

        var e2 = new CourseEnrollment("A", "C");
        e2.Submit();
        e2.Cancel();
        Assert.Equal("Cancelled", e2.CurrentStateName);

        var e3 = new CourseEnrollment("A", "C");
        e3.Submit(); e3.Pay();
        e3.Cancel();
        Assert.Equal("Cancelled", e3.CurrentStateName);
    }

    [Fact]
    public void InvalidTransition_Throws_From_Draft()
    {
        var e = new CourseEnrollment("A", "C");
        // Nu poți plăti înainte să trimiți
        Assert.Throws<InvalidOperationException>(() => e.Pay());
    }

    [Fact]
    public void Cannot_Complete_Below_100_Percent()
    {
        var e = new CourseEnrollment("A", "C");
        e.Submit(); e.Pay(); e.Start();
        e.Progress = 50;
        e.Complete();
        // Rămâne în InProgress, nu ridică excepție, doar loghează
        Assert.Equal("InProgress", e.CurrentStateName);
        Assert.Contains(e.History, h => h.Contains("Imposibil de finalizat"));
    }

    [Fact]
    public void Completed_Is_Terminal()
    {
        var e = new CourseEnrollment("A", "C");
        e.Submit(); e.Pay(); e.Start();
        e.Progress = 100;
        e.Complete();
        Assert.Throws<InvalidOperationException>(() => e.Cancel());
        Assert.Throws<InvalidOperationException>(() => e.Submit());
    }

    [Fact]
    public void History_Records_All_Transitions()
    {
        var e = new CourseEnrollment("A", "C");
        e.Submit();
        e.Pay();
        Assert.Contains(e.History, h => h.Contains("Draft → PendingPayment"));
        Assert.Contains(e.History, h => h.Contains("PendingPayment → Active"));
    }
}
