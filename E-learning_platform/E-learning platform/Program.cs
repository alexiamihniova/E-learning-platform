using E_learning_platform.Patterns.ChainOfResponsibility;
using E_learning_platform.Patterns.State;
using E_learning_platform.Patterns.Mediator;
using E_learning_platform.Patterns.TemplateMethod;
using E_learning_platform.Patterns.Visitor;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("===== E-LEARNING PLATFORM — LABORATOR 7 =====");
Console.WriteLine("Paternuri comportamentale: Chain of Responsibility, State, Mediator, Template Method, Visitor\n");

// ====================== 1. CHAIN OF RESPONSIBILITY ======================
Console.WriteLine("----- 1. CHAIN OF RESPONSIBILITY: Sistem de suport tehnic -----");

var faq = new FaqBotHandler();
var l1  = new Level1SupportHandler();
var bil = new BillingSpecialistHandler();
var tec = new TechnicalEngineerHandler();
var sec = new SecurityTeamHandler();

faq.SetNext(l1).SetNext(bil).SetNext(tec).SetNext(sec);

var requests = new[]
{
    new SupportRequest("Alexia", SupportRequestType.AccountIssue,    "Nu îmi amintesc parola.", 1),
    new SupportRequest("Andrei", SupportRequestType.CourseContent,   "Lecția 3 nu se încarcă.", 2),
    new SupportRequest("Maria",  SupportRequestType.PaymentIssue,    "Mi s-a debitat de două ori.", 3),
    new SupportRequest("Ion",    SupportRequestType.TechnicalBug,    "Eroare 500 la submit.", 4),
    new SupportRequest("Diana",  SupportRequestType.SecurityIncident,"Cont compromis.", 5),
};

foreach (var r in requests)
{
    faq.Handle(r);
    Console.WriteLine($"  [{r.RequesterName}] tipul={r.Type}, sev={r.Severity} → tratat de: {r.HandledBy}");
}

// ====================== 2. STATE ======================
Console.WriteLine("\n----- 2. STATE: Ciclul de viață al unei înscrieri la curs -----");

var enrollment = new CourseEnrollment("Alexia", "Data Integration Services");
Console.WriteLine($"  Stare: {enrollment.CurrentStateName}");
enrollment.Submit();
Console.WriteLine($"  Stare: {enrollment.CurrentStateName}");
enrollment.Pay();
Console.WriteLine($"  Stare: {enrollment.CurrentStateName}");
enrollment.Start();
Console.WriteLine($"  Stare: {enrollment.CurrentStateName}");
enrollment.Progress = 100;
enrollment.Complete();
Console.WriteLine($"  Stare finală: {enrollment.CurrentStateName}");

// Tranziție invalidă
try
{
    enrollment.Submit();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Tranziție invalidă (capturată): {ex.Message}");
}

// ====================== 3. MEDIATOR ======================
Console.WriteLine("\n----- 3. MEDIATOR: Sală de clasă virtuală -----");

var classroom = new VirtualClassroom();
var teacher   = new Teacher("Prof. Popescu", classroom);
var student1  = new Student("Alexia", classroom);
var student2  = new Student("Andrei", classroom);
var ta        = new TeachingAssistant("TA Mihai", classroom);

teacher.Announce("Lecția începe în 5 minute.");
student1.AskQuestion("Care este diferența dintre SSIS și SSRS?", teacher.Name);
teacher.Send("SSIS = integrare, SSRS = raportare.", student1.Name);

Console.WriteLine($"  Mesaje primite de Alexia: {student1.ReceivedMessages.Count}");
Console.WriteLine($"  Mesaje primite de Andrei: {student2.ReceivedMessages.Count}");
Console.WriteLine($"  Mesaje primite de TA:     {ta.ReceivedMessages.Count}");

// ====================== 4. TEMPLATE METHOD ======================
Console.WriteLine("\n----- 4. TEMPLATE METHOD: Generare rapoarte de curs -----");

var data = new CourseReportData
{
    CourseName = "Data Integration Services",
    Instructor = "Prof. Popescu",
    EnrolledStudents = 45,
    CompletedStudents = 38,
    AverageGrade = 8.7,
    TopStudents = new List<string> { "Alexia (9.8)", "Andrei (9.5)", "Maria (9.3)" }
};

CourseReportGenerator[] generators = { new HtmlReportGenerator(), new PlainTextReportGenerator(), new SmsReportGenerator() };
foreach (var gen in generators)
{
    Console.WriteLine($"  --- {gen.GetType().Name} ---");
    Console.WriteLine(gen.Generate(data));
}

// ====================== 5. VISITOR ======================
Console.WriteLine("\n----- 5. VISITOR: Export curs și calcul timp de studiu -----");

var module = new CourseModule("Modul 1: Introducere SSIS")
    .Add(new VideoLesson("Ce este SSIS?", 12, "https://..."))
    .Add(new VideoLesson("Primul pachet", 18, "https://..."))
    .Add(new PdfResource("Cheat-sheet SSIS", 6, 850_000))
    .Add(new Quiz("Test recapitulativ", 10, 100));

var jsonVisitor = new JsonExportVisitor();
module.Accept(jsonVisitor);
Console.WriteLine($"  JSON: {jsonVisitor.GetResult()}");

var xmlVisitor = new XmlExportVisitor();
module.Accept(xmlVisitor);
Console.WriteLine("  XML:");
Console.WriteLine(xmlVisitor.GetResult());

var timeCalc = new StudyTimeCalculatorVisitor();
module.Accept(timeCalc);
Console.WriteLine($"  Timp total estimat de studiu: {timeCalc.TotalMinutes} minute");

Console.WriteLine("\n===== Demo finalizat cu succes =====");
