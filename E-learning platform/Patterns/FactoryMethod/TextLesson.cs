using System;
using E_learning_platform.Patterns.Prototype;

namespace E_learning_platform.Patterns.FactoryMethod
{
    public class TextLesson : ILesson, IPrototype<TextLesson>
    {
        public string Topic { get; set; }
        public string Content { get; set; }

        public TextLesson(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public void Open()
        {
            Console.WriteLine($"[Text Lesson] Reading about: {Topic}");
            Console.WriteLine(Content);
        }

        public TextLesson Clone()
        {
            return (TextLesson)this.MemberwiseClone();
        }

        public TextLesson DeepClone()
        {
            // For strings, MemberwiseClone is sufficient as strings are immutable.
            // If we had mutable reference types, we would handle them here.
            return (TextLesson)this.MemberwiseClone();
        }
    }
}
