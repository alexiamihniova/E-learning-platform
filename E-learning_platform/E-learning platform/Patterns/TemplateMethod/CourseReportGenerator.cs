using System.Text;

namespace E_learning_platform.Patterns.TemplateMethod;

/// <summary>
/// Date despre un curs folosite de generatoarele de rapoarte.
/// </summary>
public class CourseReportData
{
    public string CourseName { get; init; } = "";
    public string Instructor { get; init; } = "";
    public int EnrolledStudents { get; init; }
    public int CompletedStudents { get; init; }
    public double AverageGrade { get; init; }
    public List<string> TopStudents { get; init; } = new();
}

/// <summary>
/// Clasa abstractă cu metoda șablon. Definește scheletul algoritmului
/// de generare a raportului. Pașii variabili sunt delegați subclaselor.
/// </summary>
public abstract class CourseReportGenerator
{
    /// <summary>
    /// METODA ȘABLON — definește pașii și ordinea lor. Sealed pentru a împiedica
    /// modificarea algoritmului în subclase (esența Template Method).
    /// </summary>
    public string Generate(CourseReportData data)
    {
        var sb = new StringBuilder();
        WriteHeader(sb, data);
        WriteSummary(sb, data);
        WriteBody(sb, data);
        if (IncludeTopStudents)
        {
            WriteTopStudents(sb, data);
        }
        WriteFooter(sb, data);
        return sb.ToString();
    }

    // Pași abstracți — fiecare format trebuie să-i implementeze
    protected abstract void WriteHeader(StringBuilder sb, CourseReportData data);
    protected abstract void WriteBody(StringBuilder sb, CourseReportData data);
    protected abstract void WriteFooter(StringBuilder sb, CourseReportData data);

    // Pas cu implementare implicită — poate fi suprascris (hook)
    protected virtual void WriteSummary(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine($"Curs: {data.CourseName} | Instructor: {data.Instructor}");
        sb.AppendLine($"Înscriși: {data.EnrolledStudents} | Finalizat: {data.CompletedStudents}");
    }

    // Hook de inclus/exclus o secțiune
    protected virtual bool IncludeTopStudents => true;

    // Pas concret — comun tuturor subclaselor (nu poate fi modificat)
    protected void WriteTopStudents(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine("Top studenți:");
        foreach (var s in data.TopStudents)
        {
            sb.AppendLine($"  - {s}");
        }
    }
}
