using E_learning_platform.Interfaces;
using System;

namespace E_learning_platform.Models
{
    public class Teacher : User, ITutor
    {
        public Teacher(int id, string name, string email) : base(id, name, email) { }

        public void CreateCourse()
        {
            // Implementation logic
            Console.WriteLine($"Teacher {Name} created a new course.");
        }

        public void Grade()
        {
            // Implementation logic
            Console.WriteLine($"Teacher {Name} is grading.");
        }

        public override string GetRole()
        {
            return "Teacher";
        }
    }
}
