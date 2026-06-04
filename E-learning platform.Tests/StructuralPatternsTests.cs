using Xunit;
using E_learning_platform.Interfaces;
using E_learning_platform.Models;
using E_learning_platform.Services;
using E_learning_platform.Patterns.Adapter;
using E_learning_platform.Patterns.Composite;
using E_learning_platform.Patterns.Facade;
using E_learning_platform.Patterns.Flyweight;
using E_learning_platform.Patterns.Decorator;
using E_learning_platform.Patterns.Bridge;
using E_learning_platform.Patterns.Proxy;
using Moq;
using System;

namespace E_learning_platform.Tests
{
    public class StructuralPatternsTests
    {
        [Fact]
        public void AdapterPattern_PayPalAdapter_AdaptsCorrectly()
        {
            var paypalApi = new PayPalApi();
            IPaymentProcessor adapter = new PayPalAdapter(paypalApi);

            bool result = adapter.ProcessPayment(100.50m);

            Assert.True(result);
        }

        [Fact]
        public void AdapterPattern_StripeAdapter_AdaptsCorrectly()
        {
            var stripeApi = new StripeApi();
            IPaymentProcessor adapter = new StripeAdapter(stripeApi);

            // StripeApi returns "SUCCESS" if amount > 0, which adapter translates to true
            bool result = adapter.ProcessPayment(250.00m);

            Assert.True(result);
        }

        [Fact]
        public void CompositePattern_CourseCategory_CalculatesTotalPrice()
        {
            var category = new CourseCategory("Programming Bundle");
            
            var course1 = new Course(1, "C# Basics", 100m, new StandardPriceStrategy());
            var course2 = new Course(2, "Advanced C#", 150m, new StandardPriceStrategy());
            
            var subCategory = new CourseCategory("Web Dev");
            var course3 = new Course(3, "ASP.NET Core", 200m, new StandardPriceStrategy());
            subCategory.Add(course3);

            category.Add(course1);
            category.Add(course2);
            category.Add(subCategory);

            // Total: 100 + 150 + 200 = 450
            decimal totalPrice = category.GetPrice();

            Assert.Equal(450m, totalPrice);
        }

        [Fact]
        public void FacadePattern_CourseEnrollmentFacade_ProcessesSuccessfully()
        {
            // Arrange
            var mockPaymentProcessor = new Mock<IPaymentProcessor>();
            mockPaymentProcessor.Setup(p => p.ProcessPayment(It.IsAny<decimal>())).Returns(true);

            var mockNotificationService = new Mock<INotificationService>();
            var enrollmentManager = new EnrollmentManager(mockNotificationService.Object);

            var facade = new CourseEnrollmentFacade(mockPaymentProcessor.Object, enrollmentManager);
            
            var student = new Student(1, "Alice", "alice@test.com");
            var course = new Course(1, "Test Course", 50m, new StandardPriceStrategy());

            // Act
            bool result = facade.BuyCourse(student, course);

            // Assert
            Assert.True(result);
            mockPaymentProcessor.Verify(p => p.ProcessPayment(50m), Times.Once);
            mockNotificationService.Verify(n => n.Notify("alice@test.com", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void FacadePattern_CourseEnrollmentFacade_FailsIfPaymentFails()
        {
            // Arrange
            var mockPaymentProcessor = new Mock<IPaymentProcessor>();
            mockPaymentProcessor.Setup(p => p.ProcessPayment(It.IsAny<decimal>())).Returns(false); // Payment fails

            var mockNotificationService = new Mock<INotificationService>();
            var enrollmentManager = new EnrollmentManager(mockNotificationService.Object);

            var facade = new CourseEnrollmentFacade(mockPaymentProcessor.Object, enrollmentManager);
            
            var student = new Student(2, "Bob", "bob@test.com");
            var course = new Course(2, "Expensive Course", 5000m, new StandardPriceStrategy());

            // Act
            bool result = facade.BuyCourse(student, course);

            // Assert
            Assert.False(result);
            mockPaymentProcessor.Verify(p => p.ProcessPayment(5000m), Times.Once);
            mockNotificationService.Verify(n => n.Notify(It.IsAny<string>(), It.IsAny<string>()), Times.Never); // Should not notify or enroll
        }
        [Fact]
        public void FlyweightPattern_CharacterFactory_ReusesInstances()
        {
            var factory = new CharacterFactory();
            
            var char1 = factory.GetCharacter('A');
            var char2 = factory.GetCharacter('B');
            var char3 = factory.GetCharacter('A');

            Assert.Same(char1, char3);
            Assert.NotSame(char1, char2);
            Assert.Equal(2, factory.GetTotalCharactersCreated());
        }

        [Fact]
        public void DecoratorPattern_SmsDecorator_CallsBaseNotifier()
        {
            var mockBaseNotifier = new Mock<INotificationService>();
            var smsDecorator = new SmsNotificationDecorator(mockBaseNotifier.Object);

            smsDecorator.Notify("user@test.com", "Hello");

            mockBaseNotifier.Verify(n => n.Notify("user@test.com", "Hello"), Times.Once);
        }

        [Fact]
        public void BridgePattern_VideoCourseMedia_UsesBrowserRenderer()
        {
            var renderer = new BrowserRenderer();
            var media = new VideoCourseMedia(renderer, "C# Advanced");

            string result = media.Play();

            Assert.Contains("Video", result);
            Assert.Contains("Browser", result);
            Assert.Contains("C# Advanced", result);
        }

        [Fact]
        public void ProxyPattern_CourseVideoProxy_DeniesAccessWhenNotEnrolled()
        {
            var proxy = new CourseVideoProxy("http://video.url", hasAccess: false);
            string result = proxy.DisplayVideo();

            Assert.Contains("Access Denied", result);
        }

        [Fact]
        public void ProxyPattern_CourseVideoProxy_AllowsAccessWhenEnrolled()
        {
            var proxy = new CourseVideoProxy("http://video.url", hasAccess: true);
            string result = proxy.DisplayVideo();

            Assert.Contains("Playing video", result);
        }
    }
}
