namespace E_learning_platform.Patterns.State
{
    public class EnrolledState : IEnrollmentState
    {
        public void AddPayment(EnrollmentContext context, decimal amount) { }
        public void ValidatePayment(EnrollmentContext context) { }
        public void CompleteEnrollment(EnrollmentContext context) { }
        public void Cancel(EnrollmentContext context)
        {
            // Refund logic could go here
            context.TransitionTo(new CancelledState());
        }

        public string GetStateName() => "Enrolled (Success)";
    }
}
