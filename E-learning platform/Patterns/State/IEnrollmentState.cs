namespace E_learning_platform.Patterns.State
{
    public interface IEnrollmentState
    {
        void AddPayment(EnrollmentContext context, decimal amount);
        void ValidatePayment(EnrollmentContext context);
        void CompleteEnrollment(EnrollmentContext context);
        void Cancel(EnrollmentContext context);
        string GetStateName();
    }
}
