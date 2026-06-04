using E_learning_platform.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_learning_platform.Patterns.Composite
{
    /// <summary>
    /// Composite Class: Represents a collection of Course Components (could be single courses or other categories).
    /// Implements ICourseComponent and forwards price calculations down the tree.
    /// </summary>
    public class CourseCategory : ICourseComponent
    {
        private readonly List<ICourseComponent> _children = new List<ICourseComponent>();
        
        public string Title { get; private set; }

        public CourseCategory(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        public void Add(ICourseComponent component)
        {
            _children.Add(component);
        }

        public void Remove(ICourseComponent component)
        {
            _children.Remove(component);
        }

        public decimal GetPrice()
        {
            // The price of a category is the sum of the prices of all its children
            return _children.Sum(c => c.GetPrice());
        }

        public void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + " Category: " + Title + $" (Total Value: {GetPrice():C})");
            foreach (var child in _children)
            {
                child.Display(depth + 2);
            }
        }
    }
}
