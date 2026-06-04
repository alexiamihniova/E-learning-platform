namespace E_learning_platform.Patterns.Mediator;

/// <summary>
/// Mediatorul concret — un „turn de control" pentru sala de clasă virtuală.
/// Gestionează rutarea mesajelor între profesor, studenți și asistent.
/// </summary>
public class VirtualClassroom : IClassroomMediator
{
    private readonly List<ClassroomParticipant> _participants = new();
    public List<string> EventLog { get; } = new();

    public void Register(ClassroomParticipant participant)
    {
        if (!_participants.Contains(participant))
        {
            _participants.Add(participant);
            EventLog.Add($"[Mediator] {participant.Name} ({participant.GetType().Name}) s-a alăturat sălii.");
        }
    }

    public void Send(string message, ClassroomParticipant sender, string? targetName = null)
    {
        if (targetName is null)
        {
            // Broadcast — toți cu excepția emitentului
            EventLog.Add($"[Broadcast] {sender.Name}: {message}");
            foreach (var p in _participants.Where(p => p != sender))
            {
                p.Receive(message, sender);
            }
        }
        else
        {
            var target = _participants.FirstOrDefault(p => p.Name == targetName);
            if (target is null)
            {
                EventLog.Add($"[Eroare] Destinatar inexistent: {targetName}.");
                return;
            }
            EventLog.Add($"[Privat] {sender.Name} → {targetName}: {message}");
            target.Receive(message, sender);
        }
    }
}

/// <summary>
/// Profesorul — poate trimite anunțuri către toți și răspunde la întrebări.
/// </summary>
public class Teacher : ClassroomParticipant
{
    public Teacher(string name, IClassroomMediator mediator) : base(name, mediator) { }

    public void Announce(string announcement) => Send($"[ANUNȚ] {announcement}");
}

/// <summary>
/// Studentul — pune întrebări și primește răspunsuri.
/// </summary>
public class Student : ClassroomParticipant
{
    public Student(string name, IClassroomMediator mediator) : base(name, mediator) { }

    public void AskQuestion(string question, string teacherName)
        => Send($"[ÎNTREBARE] {question}", teacherName);
}

/// <summary>
/// Asistentul (TA) — moderează chat-ul, primește toate broadcast-urile.
/// </summary>
public class TeachingAssistant : ClassroomParticipant
{
    public TeachingAssistant(string name, IClassroomMediator mediator) : base(name, mediator) { }
}
