namespace E_learning_platform.Models
{
    public class Enrollment
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public System.DateTime EnrollmentDate { get; private set; }

        public Enrollment(Student student, Course course)
        {
            if (student == null)
                throw new System.ArgumentNullException(nameof(student));
            if (course == null)
                throw new System.ArgumentNullException(nameof(course));

            Student = student;
            Course = course;
            EnrollmentDate = System.DateTime.Now;
        }
    }
}
