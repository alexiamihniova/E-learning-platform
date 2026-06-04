namespace E_learning_platform.Patterns.State;

/// <summary>
/// Starea inițială: studentul a creat înscrierea, dar nu a trimis-o încă.
/// </summary>
public class DraftState : EnrollmentState
{
    public override string Name => "Draft";

    public override void Submit(CourseEnrollment ctx) => ctx.TransitionTo(new PendingPaymentState());
    public override void Cancel(CourseEnrollment ctx) => ctx.TransitionTo(new CancelledState());
}

/// <summary>
/// Înscriere trimisă, așteaptă plata.
/// </summary>
public class PendingPaymentState : EnrollmentState
{
    public override string Name => "PendingPayment";

    public override void Pay(CourseEnrollment ctx) => ctx.TransitionTo(new ActiveState());
    public override void Cancel(CourseEnrollment ctx) => ctx.TransitionTo(new CancelledState());
}

/// <summary>
/// Plată confirmată — accesul la curs este activ, dar studentul nu a început încă.
/// </summary>
public class ActiveState : EnrollmentState
{
    public override string Name => "Active";

    public override void Start(CourseEnrollment ctx) => ctx.TransitionTo(new InProgressState());
    public override void Cancel(CourseEnrollment ctx) => ctx.TransitionTo(new CancelledState());
}

/// <summary>
/// Cursul este în desfășurare. Doar Complete sau Cancel sunt permise.
/// </summary>
public class InProgressState : EnrollmentState
{
    public override string Name => "InProgress";

    public override void Complete(CourseEnrollment ctx)
    {
        if (ctx.Progress < 100)
        {
            ctx.Log($"Imposibil de finalizat: progres {ctx.Progress}% < 100%.");
            return;
        }
        ctx.TransitionTo(new CompletedState());
    }

    public override void Cancel(CourseEnrollment ctx) => ctx.TransitionTo(new CancelledState());
}

/// <summary>
/// Stare finală: cursul a fost finalizat cu succes.
/// </summary>
public class CompletedState : EnrollmentState
{
    public override string Name => "Completed";
    // Niciun tranziție permisă — stare terminală
}

/// <summary>
/// Stare finală: înscriere anulată.
/// </summary>
public class CancelledState : EnrollmentState
{
    public override string Name => "Cancelled";
    // Niciun tranziție permisă — stare terminală
}
