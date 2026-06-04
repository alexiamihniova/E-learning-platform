using E_learning_platform.Patterns.Visitor;
using Xunit;

namespace E_learning_platform.Tests.PatternTests;

public class VisitorTests
{
    private static CourseModule BuildSampleModule() =>
        new CourseModule("M1")
            .Add(new VideoLesson("L1", 10, "url1"))
            .Add(new VideoLesson("L2", 20, "url2"))
            .Add(new PdfResource("R1", 5, 1000))
            .Add(new Quiz("Q1", 8, 80));

    [Fact]
    public void JsonExport_Includes_All_Element_Types()
    {
        var v = new JsonExportVisitor();
        BuildSampleModule().Accept(v);
        var json = v.GetResult();
        Assert.Contains("\"type\":\"module\"", json);
        Assert.Contains("\"type\":\"video\"", json);
        Assert.Contains("\"type\":\"pdf\"", json);
        Assert.Contains("\"type\":\"quiz\"", json);
    }

    [Fact]
    public void XmlExport_Wraps_Output_In_CourseTag()
    {
        var v = new XmlExportVisitor();
        BuildSampleModule().Accept(v);
        var xml = v.GetResult();
        Assert.StartsWith("<course>", xml);
        Assert.EndsWith("</course>", xml);
        Assert.Contains("<video", xml);
        Assert.Contains("<quiz", xml);
        Assert.Contains("<pdf", xml);
    }

    [Fact]
    public void StudyTimeCalculator_Sums_Correct_Total()
    {
        var v = new StudyTimeCalculatorVisitor();
        BuildSampleModule().Accept(v);
        // 10 + 20 (video) + 5*3 = 15 (pdf) + 8*2 = 16 (quiz) = 61
        Assert.Equal(61, v.TotalMinutes);
    }

    [Fact]
    public void Visitor_Traverses_Nested_Modules()
    {
        var inner = new CourseModule("Submodul")
            .Add(new VideoLesson("V_inner", 5, "u"));
        var outer = new CourseModule("Modul Principal")
            .Add(new VideoLesson("V_outer", 10, "u"))
            .Add(inner);

        var v = new StudyTimeCalculatorVisitor();
        outer.Accept(v);
        Assert.Equal(15, v.TotalMinutes);   // 10 + 5
    }

    [Fact]
    public void Adding_New_Visitor_Does_Not_Require_Modifying_Elements()
    {
        // Demonstrarea principalului avantaj: extensibilitate fără modificarea structurii.
        // Dacă această clasă internă vizitatoare poate funcționa pe aceleași elemente
        // fără a le modifica, înseamnă că separarea algoritm/structură este corectă.
        var counter = new ElementCounterVisitor();
        BuildSampleModule().Accept(counter);
        Assert.Equal(1, counter.ModuleCount);
        Assert.Equal(2, counter.VideoCount);
        Assert.Equal(1, counter.PdfCount);
        Assert.Equal(1, counter.QuizCount);
    }

    /// <summary>
    /// Un vizitator definit local doar pentru test, care numără elementele.
    /// Faptul că poate fi adăugat fără modificări în CourseElements demonstrează
    /// extensibilitatea Visitor-ului.
    /// </summary>
    private class ElementCounterVisitor : ICourseVisitor
    {
        public int ModuleCount;
        public int VideoCount;
        public int PdfCount;
        public int QuizCount;
        public void Visit(VideoLesson lesson) => VideoCount++;
        public void Visit(Quiz quiz) => QuizCount++;
        public void Visit(PdfResource pdf) => PdfCount++;
        public void Visit(CourseModule module) => ModuleCount++;
    }
}
