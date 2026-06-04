using E_learning_platform.Patterns.Mediator;
using Xunit;

namespace E_learning_platform.Tests.PatternTests;

public class MediatorTests
{
    [Fact]
    public void Broadcast_Reaches_All_Except_Sender()
    {
        var room = new VirtualClassroom();
        var t = new Teacher("T", room);
        var s1 = new Student("S1", room);
        var s2 = new Student("S2", room);

        t.Announce("Salut!");

        Assert.Single(s1.ReceivedMessages);
        Assert.Single(s2.ReceivedMessages);
        Assert.Empty(t.ReceivedMessages);
    }

    [Fact]
    public void PrivateMessage_Reaches_Only_Target()
    {
        var room = new VirtualClassroom();
        var t = new Teacher("T", room);
        var s1 = new Student("S1", room);
        var s2 = new Student("S2", room);

        s1.AskQuestion("?", "T");

        Assert.Single(t.ReceivedMessages);
        Assert.Empty(s2.ReceivedMessages);
    }

    [Fact]
    public void Unknown_Target_Does_Not_Crash_And_Logs_Error()
    {
        var room = new VirtualClassroom();
        var s = new Student("S", room);

        s.Send("Mesaj", "Inexistent");

        Assert.Contains(room.EventLog, e => e.Contains("Eroare") && e.Contains("Inexistent"));
    }

    [Fact]
    public void Participants_Are_Registered_On_Construction()
    {
        var room = new VirtualClassroom();
        _ = new Teacher("T", room);
        _ = new Student("S", room);
        _ = new TeachingAssistant("TA", room);

        Assert.Equal(3, room.EventLog.Count(e => e.Contains("s-a alăturat")));
    }

    [Fact]
    public void Participants_Do_Not_Communicate_Directly()
    {
        // Test conceptual: participanții au doar referință la mediator, nu unul la altul
        var room = new VirtualClassroom();
        var t = new Teacher("T", room);
        var s = new Student("S", room);

        // Niciuna din clase nu expune o referință publică către cealaltă
        var teacherFields = typeof(Teacher).GetFields(
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
        Assert.DoesNotContain(teacherFields, f => f.FieldType == typeof(Student));
    }

    [Fact]
    public void Teacher_Reply_Reaches_Only_The_Asking_Student()
    {
        var room = new VirtualClassroom();
        var t = new Teacher("T", room);
        var s1 = new Student("S1", room);
        var s2 = new Student("S2", room);

        s1.AskQuestion("Ce este SSIS?", "T");
        t.Send("Integration Services.", "S1");

        Assert.Equal(1, s1.ReceivedMessages.Count);
        Assert.Empty(s2.ReceivedMessages);
    }
}
