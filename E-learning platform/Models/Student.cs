using E_learning_platform.Interfaces;
using System;

namespace E_learning_platform.Models
{
    public class Student : User, ILearner
    {
        public Student(int id, string name, string email) : base(id, name, email) { }

        public void Subscribe()
        {
            // Implementation logic
            Console.WriteLine($"Student {Name} has subscribed.");
        }

        public void Watch()
        {
            // Implementation logic
            Console.WriteLine($"Student {Name} is watching a course.");
        }

        public override string GetRole()
        {
            return "Student";
        }
    }
}
