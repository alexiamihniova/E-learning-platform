namespace E_learning_platform.Patterns.ChainOfResponsibility;

/// <summary>
/// Clasa de bază abstractă pentru toți handlerii din lanțul de responsabilitate.
/// Fiecare handler decide dacă tratează cererea sau o trimite mai departe.
/// </summary>
public abstract class SupportHandler
{
    private SupportHandler? _next;

    /// <summary>
    /// Setează următorul handler în lanț. Returnează handlerul primit pentru a permite înlănțuirea fluentă.
    /// </summary>
    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    /// <summary>
    /// Punctul de intrare. Dacă handlerul curent nu poate trata cererea, o pasează mai departe.
    /// </summary>
    public void Handle(SupportRequest request)
    {
        if (CanHandle(request))
        {
            Process(request);
        }
        else if (_next is not null)
        {
            _next.Handle(request);
        }
        else
        {
            request.Resolution = "Cererea nu a putut fi tratată — escaladare manuală necesară.";
        }
    }

    protected abstract bool CanHandle(SupportRequest request);
    protected abstract void Process(SupportRequest request);
}
