namespace E_learning_platform.Patterns.Visitor;

/// <summary>
/// Interfața element. Orice nod de conținut din curs trebuie să accepte vizitatori.
/// </summary>
public interface ICourseElement
{
    void Accept(ICourseVisitor visitor);
}

/// <summary>
/// O lecție video — conține titlu, durată în minute și URL.
/// </summary>
public class VideoLesson : ICourseElement
{
    public string Title { get; }
    public int DurationMinutes { get; }
    public string Url { get; }

    public VideoLesson(string title, int durationMinutes, string url)
    {
        Title = title;
        DurationMinutes = durationMinutes;
        Url = url;
    }

    public void Accept(ICourseVisitor visitor) => visitor.Visit(this);
}

/// <summary>
/// Test de tip quiz — conține număr de întrebări și punctaj maxim.
/// </summary>
public class Quiz : ICourseElement
{
    public string Title { get; }
    public int QuestionCount { get; }
    public int MaxScore { get; }

    public Quiz(string title, int questionCount, int maxScore)
    {
        Title = title;
        QuestionCount = questionCount;
        MaxScore = maxScore;
    }

    public void Accept(ICourseVisitor visitor) => visitor.Visit(this);
}

/// <summary>
/// Resursă PDF — material descărcabil.
/// </summary>
public class PdfResource : ICourseElement
{
    public string Title { get; }
    public int PageCount { get; }
    public long SizeBytes { get; }

    public PdfResource(string title, int pageCount, long sizeBytes)
    {
        Title = title;
        PageCount = pageCount;
        SizeBytes = sizeBytes;
    }

    public void Accept(ICourseVisitor visitor) => visitor.Visit(this);
}

/// <summary>
/// Modul — un container care grupează mai multe elemente de curs.
/// </summary>
public class CourseModule : ICourseElement
{
    public string Title { get; }
    public List<ICourseElement> Children { get; } = new();

    public CourseModule(string title) => Title = title;

    public CourseModule Add(ICourseElement child)
    {
        Children.Add(child);
        return this;
    }

    public void Accept(ICourseVisitor visitor)
    {
        visitor.Visit(this);
        foreach (var child in Children)
        {
            child.Accept(visitor);
        }
    }
}
