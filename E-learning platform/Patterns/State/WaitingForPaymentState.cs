namespace E_learning_platform.Patterns.State
{
    public class WaitingForPaymentState : IEnrollmentState
    {
        public void AddPayment(EnrollmentContext context, decimal amount)
        {
            if (context.TotalPaid >= context.RequiredAmount)
            {
                context.TransitionTo(new PaymentValidationState());
            }
        }

        public void ValidatePayment(EnrollmentContext context)
        {
            // Payment not yet sufficient
        }

        public void CompleteEnrollment(EnrollmentContext context)
        {
            // Must pay first
        }

        public void Cancel(EnrollmentContext context)
        {
            context.TransitionTo(new CancelledState());
        }

        public string GetStateName() => "Waiting for Payment";
    }
}
