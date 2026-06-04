using System;
using System.Collections.Generic;
using E_learning_platform.Patterns.Prototype;

namespace E_learning_platform.Patterns.FactoryMethod
{
    public class VideoLesson : ILesson, IPrototype<VideoLesson>
    {
        // REFACTORING 1: Use auto-property
        public string Title { get; private set; }
        public List<string> Tags { get; set; } = new List<string>();

        public VideoLesson(string title)
        {
            Title = title;
        }

        public void Open()
        {
            Console.WriteLine($"[Video Lesson] Playing video for: {Title}. Tags: {string.Join(", ", Tags)}");
        }

        // Shallow Copy
        public VideoLesson Clone()
        {
            return (VideoLesson)this.MemberwiseClone();
        }

        // Deep Copy
        public VideoLesson DeepClone()
        {
            var clone = (VideoLesson)this.MemberwiseClone();
            clone.Tags = new List<string>(this.Tags); // Deep copy of the list
            return clone;
        }
    }
}
