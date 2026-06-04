using System.Text;

namespace E_learning_platform.Patterns.Visitor;

/// <summary>
/// Interfața vizitatorului — declară o operație Visit pentru fiecare tip concret de element.
/// </summary>
public interface ICourseVisitor
{
    void Visit(VideoLesson lesson);
    void Visit(Quiz quiz);
    void Visit(PdfResource pdf);
    void Visit(CourseModule module);
}

/// <summary>
/// Vizitator care exportă structura cursului în format JSON.
/// </summary>
public class JsonExportVisitor : ICourseVisitor
{
    private readonly StringBuilder _sb = new();
    private bool _first = true;

    public JsonExportVisitor() => _sb.Append('[');

    public string GetResult() => _sb.ToString() + "]";

    private void Comma()
    {
        if (!_first) _sb.Append(',');
        _first = false;
    }

    public void Visit(VideoLesson lesson)
    {
        Comma();
        _sb.Append($"{{\"type\":\"video\",\"title\":\"{lesson.Title}\",\"duration\":{lesson.DurationMinutes}}}");
    }

    public void Visit(Quiz quiz)
    {
        Comma();
        _sb.Append($"{{\"type\":\"quiz\",\"title\":\"{quiz.Title}\",\"questions\":{quiz.QuestionCount}}}");
    }

    public void Visit(PdfResource pdf)
    {
        Comma();
        _sb.Append($"{{\"type\":\"pdf\",\"title\":\"{pdf.Title}\",\"pages\":{pdf.PageCount}}}");
    }

    public void Visit(CourseModule module)
    {
        Comma();
        _sb.Append($"{{\"type\":\"module\",\"title\":\"{module.Title}\"}}");
    }
}

/// <summary>
/// Vizitator care exportă cursul în format XML.
/// </summary>
public class XmlExportVisitor : ICourseVisitor
{
    private readonly StringBuilder _sb = new();

    public XmlExportVisitor() => _sb.AppendLine("<course>");

    public string GetResult() => _sb.ToString() + "</course>";

    public void Visit(VideoLesson lesson)
        => _sb.AppendLine($"  <video title=\"{lesson.Title}\" duration=\"{lesson.DurationMinutes}\"/>");

    public void Visit(Quiz quiz)
        => _sb.AppendLine($"  <quiz title=\"{quiz.Title}\" questions=\"{quiz.QuestionCount}\"/>");

    public void Visit(PdfResource pdf)
        => _sb.AppendLine($"  <pdf title=\"{pdf.Title}\" pages=\"{pdf.PageCount}\"/>");

    public void Visit(CourseModule module)
        => _sb.AppendLine($"  <module title=\"{module.Title}\"/>");
}

/// <summary>
/// Vizitator care calculează durata totală de studiu (în minute) parcurgând toate elementele.
/// Demonstrează că un nou algoritm a fost adăugat fără a modifica clasele de elemente.
/// </summary>
public class StudyTimeCalculatorVisitor : ICourseVisitor
{
    public int TotalMinutes { get; private set; }

    // Estimări pentru calcul:
    private const int MinutesPerQuizQuestion = 2;
    private const int MinutesPerPdfPage = 3;

    public void Visit(VideoLesson lesson) => TotalMinutes += lesson.DurationMinutes;
    public void Visit(Quiz quiz) => TotalMinutes += quiz.QuestionCount * MinutesPerQuizQuestion;
    public void Visit(PdfResource pdf) => TotalMinutes += pdf.PageCount * MinutesPerPdfPage;
    public void Visit(CourseModule module) { /* containerul în sine nu adaugă timp */ }
}
