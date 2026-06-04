using E_learning_platform.Models;
using E_learning_platform.Interfaces;
using System.Collections.Generic;

namespace E_learning_platform.Patterns.Builder
{
    public class CourseBuilder : ICourseBuilder
    {
        private string _title = "Untitled";
        private decimal _basePrice = 0;
        private List<string> _modules = new List<string>();
        private readonly IPriceStrategy _defaultPriceStrategy;

        public CourseBuilder(IPriceStrategy defaultPriceStrategy)
        {
            _defaultPriceStrategy = defaultPriceStrategy;
            Reset();
        }

        public void Reset()
        {
            _title = "Untitled Course";
            _basePrice = 0;
            _modules = new List<string>();
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public void SetPrice(decimal price)
        {
            _basePrice = price;
        }

        public void AddModule(string module)
        {
            if (!string.IsNullOrWhiteSpace(module))
            {
                _modules.Add(module);
            }
        }

        public Course GetCourse()
        {
            // For simplicity, we are generating a random ID or using a static counter could be better, 
            // but for this pattern demonstration, 0 or a placeholder is fine.
            var course = new Course(0, _title, _basePrice, _defaultPriceStrategy);
            
            foreach (var module in _modules)
            {
                course.AddModule(module);
            }
            
            Course result = course;
            
            Reset();
            
            return result;
        }
    }
}
