namespace E_learning_platform.Patterns.State;

/// <summary>
/// Contextul: înscrierea unui student la un curs.
/// Comportamentul fiecărei acțiuni depinde de starea curentă.
/// </summary>
public class CourseEnrollment
{
    private EnrollmentState _state;

    public string StudentName { get; }
    public string CourseName { get; }
    public int Progress { get; set; }   // procentaj 0–100
    public List<string> History { get; } = new();

    public CourseEnrollment(string studentName, string courseName)
    {
        StudentName = studentName;
        CourseName = courseName;
        _state = new DraftState();
        Log("Înscriere creată în starea Draft.");
    }

    public string CurrentStateName => _state.Name;

    internal void TransitionTo(EnrollmentState newState)
    {
        Log($"Tranziție: {_state.Name} → {newState.Name}");
        _state = newState;
    }

    public void Log(string message) => History.Add(message);

    // Acțiunile delegate stării curente
    public void Submit()    => _state.Submit(this);
    public void Pay()       => _state.Pay(this);
    public void Start()     => _state.Start(this);
    public void Complete()  => _state.Complete(this);
    public void Cancel()    => _state.Cancel(this);
}

/// <summary>
/// Clasa abstractă pentru orice stare a înscrierii.
/// Implementarea implicită aruncă InvalidOperationException pentru tranziții invalide.
/// </summary>
public abstract class EnrollmentState
{
    public abstract string Name { get; }

    public virtual void Submit(CourseEnrollment ctx)   => Invalid(ctx, nameof(Submit));
    public virtual void Pay(CourseEnrollment ctx)      => Invalid(ctx, nameof(Pay));
    public virtual void Start(CourseEnrollment ctx)    => Invalid(ctx, nameof(Start));
    public virtual void Complete(CourseEnrollment ctx) => Invalid(ctx, nameof(Complete));
    public virtual void Cancel(CourseEnrollment ctx)   => Invalid(ctx, nameof(Cancel));

    private void Invalid(CourseEnrollment ctx, string action)
        => throw new InvalidOperationException(
            $"Acțiunea '{action}' nu este permisă în starea '{Name}' pentru cursul '{ctx.CourseName}'.");
}
