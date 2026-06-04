using Xunit;
using E_learning_platform.Patterns.Builder;
using E_learning_platform.Patterns.FactoryMethod;
using E_learning_platform.Patterns.Singleton;
using E_learning_platform.Models;
using E_learning_platform.Interfaces;
using System.Collections.Generic;

namespace E_learning_platform.Tests
{
    public class CreationalPatternsTests
    {
        // --- Builder Pattern Tests ---

        [Fact]
        public void CourseBuilder_ShouldCreateCourseWithCorrectProperties()
        {
            // Arrange
            var mockPriceStrategy = new MockPriceStrategy();
            var builder = new CourseBuilder(mockPriceStrategy);

            // Act
            builder.SetTitle("Test Course");
            builder.SetPrice(100m);
            builder.AddModule("Module 1");
            var course = builder.GetCourse();

            // Assert
            Assert.Equal("Test Course", course.Title);
            Assert.Equal(100m, course.BasePrice);
            Assert.Contains("Module 1", course.Modules);
        }

        [Fact]
        public void CourseDirector_ShouldConstructPremiumCourse()
        {
            // Arrange
            var mockPriceStrategy = new MockPriceStrategy();
            var builder = new CourseBuilder(mockPriceStrategy);
            var director = new CourseDirector(builder);

            // Act
            director.ConstructPremiumCourse();
            var course = builder.GetCourse();

            // Assert
            Assert.Equal("Premium C# Masterclass", course.Title);
            Assert.Equal(150m, course.BasePrice);
            Assert.True(course.Modules.Count >= 5);
        }

        // --- Prototype Pattern Tests ---

        [Fact]
        public void VideoLesson_DeepClone_ShouldCreateIndependentTags()
        {
            // Arrange
            var original = new VideoLesson("Original Video");
            original.Tags.Add("C#");
            original.Tags.Add("Programming");

            // Act
            var clone = original.DeepClone();
            clone.Tags.Add("CloneTag");

            // Assert
            Assert.NotSame(original, clone);
            Assert.Equal(original.Title, clone.Title);
            Assert.Contains("CloneTag", clone.Tags);
            Assert.DoesNotContain("CloneTag", original.Tags); // Verify independence
        }

        [Fact]
        public void TextLesson_Clone_ShouldCreateCopy()
        {
            // Arrange
            var original = new TextLesson("Topic 1", "Content 1");

            // Act
            var clone = original.Clone();

            // Assert
            Assert.NotSame(original, clone);
            Assert.Equal(original.Topic, clone.Topic);
            Assert.Equal(original.Content, clone.Content);
        }

        // --- Singleton Pattern Tests ---

        [Fact]
        public void DatabaseConnection_Instance_ShouldReturnSameObject()
        {
            // Act
            var instance1 = DatabaseConnection.Instance;
            var instance2 = DatabaseConnection.Instance;

            // Assert
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void DatabaseConnection_Connect_ShouldChangeState()
        {
            // Arrange
            var db = DatabaseConnection.Instance;
            if (db.IsConnected) db.Disconnect(); // Ensure disconnected start

            // Act
            db.Connect();

            // Assert
            Assert.True(db.IsConnected);
            
            // Cleanup
            db.Disconnect();
        }

        // Mock class for dependency
        public class MockPriceStrategy : IPriceStrategy
        {
            public decimal CalculatePrice(decimal basePrice)
            {
                return basePrice;
            }
        }
    }
}
