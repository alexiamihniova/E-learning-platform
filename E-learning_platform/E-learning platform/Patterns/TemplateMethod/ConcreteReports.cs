using System.Text;

namespace E_learning_platform.Patterns.TemplateMethod;

/// <summary>
/// Raport în format HTML — pentru publicare pe pagina de admin a platformei.
/// </summary>
public class HtmlReportGenerator : CourseReportGenerator
{
    protected override void WriteHeader(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine("<html><head><title>Raport curs</title></head><body>");
        sb.AppendLine($"<h1>Raport: {data.CourseName}</h1>");
    }

    protected override void WriteSummary(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine("<section class='summary'>");
        sb.AppendLine($"<p><b>Instructor:</b> {data.Instructor}</p>");
        sb.AppendLine($"<p><b>Înscriși:</b> {data.EnrolledStudents} | <b>Finalizat:</b> {data.CompletedStudents}</p>");
        sb.AppendLine("</section>");
    }

    protected override void WriteBody(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine($"<p>Media generală: <strong>{data.AverageGrade:F2}</strong></p>");
    }

    protected override void WriteFooter(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine($"<footer>Generat la {DateTime.Now:yyyy-MM-dd}</footer>");
        sb.AppendLine("</body></html>");
    }
}

/// <summary>
/// Raport simplu de tip text — pentru email sau export rapid.
/// </summary>
public class PlainTextReportGenerator : CourseReportGenerator
{
    protected override void WriteHeader(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine(new string('=', 50));
        sb.AppendLine($"RAPORT CURS: {data.CourseName.ToUpper()}");
        sb.AppendLine(new string('=', 50));
    }

    protected override void WriteBody(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine($"Media generală: {data.AverageGrade:F2}");
    }

    protected override void WriteFooter(StringBuilder sb, CourseReportData data)
    {
        sb.AppendLine(new string('-', 50));
        sb.AppendLine($"Generat la: {DateTime.Now:yyyy-MM-dd HH:mm}");
    }
}

/// <summary>
/// Raport scurt pentru SMS — exclude lista de top studenți (folosind hook-ul).
/// </summary>
public class SmsReportGenerator : CourseReportGenerator
{
    protected override bool IncludeTopStudents => false;   // hook suprascris

    protected override void WriteHeader(StringBuilder sb, CourseReportData data)
    {
        sb.Append($"[E-Learning] {data.CourseName}: ");
    }

    protected override void WriteSummary(StringBuilder sb, CourseReportData data)
    {
        sb.Append($"{data.CompletedStudents}/{data.EnrolledStudents} finalizat, ");
    }

    protected override void WriteBody(StringBuilder sb, CourseReportData data)
    {
        sb.Append($"medie {data.AverageGrade:F1}. ");
    }

    protected override void WriteFooter(StringBuilder sb, CourseReportData data)
    {
        sb.Append("Detalii pe portal.");
    }
}
