using Xunit;
using E_learning_platform.Patterns.FactoryMethod;
using E_learning_platform.Patterns.AbstractFactory;
using System;

namespace E_learning_platform.Tests
{
    public class PatternTests
    {
        // ==========================================
        // Factory Method Tests
        // ==========================================
        [Fact]
        public void VideoLessonFactory_CreateLesson_ReturnsVideoLesson_WithCorrectTitle()
        {
            // Arrange
            LessonFactory factory = new VideoLessonFactory();
            string expectedTitle = "C# Design Patterns";

            // Act
            ILesson lesson = factory.CreateLesson(expectedTitle);

            // Assert
            Assert.IsType<VideoLesson>(lesson);
            
            // Note: Since we haven't exposed Title property yet for verification, 
            // the cast is enough for this step. For stricter test, we should refactor to expose Title.
            var videoLesson = lesson as VideoLesson;
            Assert.NotNull(videoLesson);
            Assert.Equal(expectedTitle, videoLesson.Title);
        }

        // ==========================================
        // Abstract Factory Tests
        // ==========================================
        [Fact]
        public void HonorsAwardFactory_CreateCertificate_ReturnsPhysicalCertificate()
        {
            // Arrange
            IAwardFactory factory = new HonorsAwardFactory();

            // Act
            ICertificate certificate = factory.CreateCertificate();

            // Assert
            // Use Assert.IsType to check if it's the specific concrete implementation
            Assert.IsType<PhysicalCertificate>(certificate);
        }

        [Fact]
        public void StandardAwardFactory_CreateBadge_ReturnsBronzeBadge()
        {
            // Arrange
            IAwardFactory factory = new StandardAwardFactory();

            // Act
            IBadge badge = factory.CreateBadge();

            // Assert
            Assert.IsType<BronzeBadge>(badge);
        }
    }
}
