namespace E_learning_platform.Patterns.Mediator;

/// <summary>
/// Interfața mediatorului — definește contractul prin care participanții trimit notificări.
/// </summary>
public interface IClassroomMediator
{
    void Send(string message, ClassroomParticipant sender, string? targetName = null);
    void Register(ClassroomParticipant participant);
}

/// <summary>
/// Participant abstract: un actor în sala de clasă virtuală.
/// Nu cunoaște direct ceilalți participanți, ci doar mediatorul.
/// </summary>
public abstract class ClassroomParticipant
{
    public string Name { get; }
    protected IClassroomMediator Mediator { get; }
    public List<string> ReceivedMessages { get; } = new();

    protected ClassroomParticipant(string name, IClassroomMediator mediator)
    {
        Name = name;
        Mediator = mediator;
        Mediator.Register(this);
    }

    /// <summary>
    /// Trimite un mesaj — fie public (broadcast), fie privat dacă targetName e setat.
    /// </summary>
    public void Send(string message, string? targetName = null)
    {
        Mediator.Send(message, this, targetName);
    }

    /// <summary>
    /// Apelat de mediator când un mesaj ajunge la acest participant.
    /// </summary>
    public virtual void Receive(string message, ClassroomParticipant sender)
    {
        ReceivedMessages.Add($"De la {sender.Name}: {message}");
    }
}
