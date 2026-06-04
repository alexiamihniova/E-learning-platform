using E_learning_platform.Patterns.FactoryMethod;
using E_learning_platform.Patterns.AbstractFactory;
using E_learning_platform.Patterns.Adapter;
using E_learning_platform.Patterns.Composite;
using E_learning_platform.Patterns.Facade;
using E_learning_platform.Patterns.Flyweight;
using E_learning_platform.Patterns.Decorator;
using E_learning_platform.Patterns.Bridge;
using E_learning_platform.Patterns.Proxy;
using E_learning_platform.Models;
using E_learning_platform.Services;
using E_learning_platform.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



// ==========================================
// Lab 2: Creational Design Patterns Demonstration
// ==========================================

Console.WriteLine("\n--- Lab 2: Factory Method Pattern ---");
// Scenario 1: Create a Video Lesson without knowing the concrete class
LessonFactory lessonFactory = new VideoLessonFactory();
ILesson myLesson = lessonFactory.CreateLesson("Design Patterns 101");
myLesson.Open(); // Output: Playing video...

// We can easily switch to TextLesson by changing the factory:
// lessonFactory = new TextLessonFactory();


Console.WriteLine("\n--- Lab 2: Abstract Factory Pattern ---");
// Scenario 2: Student gets Honors
Console.WriteLine("Student achieved > 90% score! Granting Honors Awards...");

// Create the Honors factory (family of products)
IAwardFactory awardFactory = new HonorsAwardFactory();

// Create the products using the factory
ICertificate cert = awardFactory.CreateCertificate();
IBadge badge = awardFactory.CreateBadge();

// Use them
cert.Print(); // Output: Physical Certificate...
badge.Wear(); // Output: Gold Badge...

Console.WriteLine("------------------------------------------\n");

// ==========================================
// Lab 4 & 5: Structural Design Patterns Demonstration
// ==========================================

Console.WriteLine("\n--- Lab 4: Adapter Pattern ---");
var stripeApi = new StripeApi();
IPaymentProcessor adapter = new StripeAdapter(stripeApi);
bool paymentResult = adapter.ProcessPayment(250.0m);
Console.WriteLine($"Stripe Payment Processed: {paymentResult}");

Console.WriteLine("\n--- Lab 4: Composite Pattern ---");
var category = new CourseCategory("Programare Avansată");
category.Add(new Course(1, "C# Advanced", 200m, new StandardPriceStrategy()));
category.Add(new Course(2, "Design Patterns", 150m, new StandardPriceStrategy()));
Console.WriteLine($"Preț total categorie (cursuri componente): {category.GetPrice()}");

Console.WriteLine("\n--- Lab 4: Facade Pattern ---");
INotificationService emailService = new EmailService();
var enrollmentManager = new EnrollmentManager(emailService);
var facade = new CourseEnrollmentFacade(adapter, enrollmentManager);
var student = new Student(1, "Ion Popescu", "ion@test.com");
var course = new Course(3, "Arhitectură Software", 300m, new StandardPriceStrategy());
bool enrollResult = facade.BuyCourse(student, course);
Console.WriteLine($"Rezultat înscriere Facade: {enrollResult}");

Console.WriteLine("\n--- Lab 5: Flyweight Pattern ---");
var charFactory = new CharacterFactory();
var char1 = charFactory.GetCharacter('A');
var char2 = charFactory.GetCharacter('A');
char1.Draw("Arial", 12);
char2.Draw("Times New Roman", 14);
Console.WriteLine($"Instanțe litera 'A' unice în memorie: {ReferenceEquals(char1, char2)} | Total caractere create: {charFactory.GetTotalCharactersCreated()}");

Console.WriteLine("\n--- Lab 5: Decorator Pattern ---");
INotificationService baseEmail = new EmailService();
INotificationService smsNotification = new SmsNotificationDecorator(baseEmail);
INotificationService pushNotification = new PushNotificationDecorator(smsNotification); // Adăugare de 2 decoratori
pushNotification.Notify("ion@test.com", "Bine ai venit la curs!");

Console.WriteLine("\n--- Lab 5: Bridge Pattern ---");
IRenderer webRenderer = new BrowserRenderer();
MediaResource videoMedia = new VideoCourseMedia(webRenderer, "Introducere în Design Patterns");
Console.WriteLine(videoMedia.Play());

Console.WriteLine("\n--- Lab 5: Proxy Pattern ---");
ICourseVideo proxyVideoNoAccess = new CourseVideoProxy("https://myserver.com/video1.mp4", hasAccess: false);
Console.WriteLine($"Fără acces: {proxyVideoNoAccess.DisplayVideo()}");

ICourseVideo proxyVideoWithAccess = new CourseVideoProxy("https://myserver.com/video1.mp4", hasAccess: true);
Console.WriteLine($"Cu acces: {proxyVideoWithAccess.DisplayVideo()}");

Console.WriteLine("------------------------------------------\n");

app.Run();
