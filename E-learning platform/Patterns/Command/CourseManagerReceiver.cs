using System;
using System.Collections.Generic;

namespace E_learning_platform.Patterns.Command
{
    public class CourseManagerReceiver
    {
        private List<string> _enrolledStudents = new List<string>();

        public void EnrollStudent(string studentName, string courseName)
        {
            _enrolledStudents.Add(studentName);
            Console.WriteLine($"{studentName} has been enrolled in {courseName}.");
        }

        public void DropStudent(string studentName, string courseName)
        {
            _enrolledStudents.Remove(studentName);
            Console.WriteLine($"{studentName} has been dropped from {courseName}.");
        }

        public bool IsEnrolled(string studentName)
        {
            return _enrolledStudents.Contains(studentName);
        }
    }
}
