namespace E_learning_platform.Patterns.State
{
    public class CancelledState : IEnrollmentState
    {
        public void AddPayment(EnrollmentContext context, decimal amount) { }
        public void ValidatePayment(EnrollmentContext context) { }
        public void CompleteEnrollment(EnrollmentContext context) { }
        public void Cancel(EnrollmentContext context) { }

        public string GetStateName() => "Cancelled";
    }
}
