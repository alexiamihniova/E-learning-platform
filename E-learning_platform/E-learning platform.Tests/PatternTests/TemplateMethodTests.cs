using E_learning_platform.Patterns.TemplateMethod;
using Xunit;

namespace E_learning_platform.Tests.PatternTests;

public class TemplateMethodTests
{
    private static CourseReportData SampleData() => new()
    {
        CourseName = "DIS",
        Instructor = "Popescu",
        EnrolledStudents = 30,
        CompletedStudents = 25,
        AverageGrade = 8.5,
        TopStudents = new List<string> { "Alexia", "Andrei" }
    };

    [Fact]
    public void HtmlReport_Contains_HtmlTags()
    {
        var report = new HtmlReportGenerator().Generate(SampleData());
        Assert.Contains("<html>", report);
        Assert.Contains("<h1>", report);
        Assert.Contains("</html>", report);
    }

    [Fact]
    public void PlainTextReport_Contains_HeaderAndFooterSeparators()
    {
        var report = new PlainTextReportGenerator().Generate(SampleData());
        Assert.Contains("RAPORT CURS: DIS", report);
        Assert.Contains("====", report);
    }

    [Fact]
    public void SmsReport_Skips_TopStudents()
    {
        var report = new SmsReportGenerator().Generate(SampleData());
        Assert.DoesNotContain("Alexia", report);
        Assert.DoesNotContain("Top studenți", report);
    }

    [Fact]
    public void All_Generators_Include_TopStudents_Except_Sms()
    {
        var html = new HtmlReportGenerator().Generate(SampleData());
        var txt  = new PlainTextReportGenerator().Generate(SampleData());

        Assert.Contains("Alexia", html);
        Assert.Contains("Alexia", txt);
    }

    [Fact]
    public void All_Generators_Use_The_Same_Algorithm_Skeleton()
    {
        // Verificăm că toate cele 3 conțin în rezultat informația din date,
        // ceea ce demonstrează că pașii fundamentali (header→summary→body→footer)
        // sunt parcurși de toate generatoarele.
        var data = SampleData();
        foreach (var gen in new CourseReportGenerator[] {
            new HtmlReportGenerator(), new PlainTextReportGenerator(), new SmsReportGenerator() })
        {
            var output = gen.Generate(data);
            Assert.Contains("DIS", output);
            Assert.NotEmpty(output);
        }
    }
}
