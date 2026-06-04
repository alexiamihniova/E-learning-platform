namespace E_learning_platform.Patterns.State
{
    public class PaymentValidationState : IEnrollmentState
    {
        public void AddPayment(EnrollmentContext context, decimal amount)
        {
            // Payment already sufficient, additional payment recorded
        }

        public void ValidatePayment(EnrollmentContext context)
        {
            if (context.TotalPaid >= context.RequiredAmount)
            {
                context.TransitionTo(new EnrolledState());
            }
            else
            {
                context.TransitionTo(new WaitingForPaymentState());
            }
        }

        public void CompleteEnrollment(EnrollmentContext context)
        {
            // Must validate first
        }

        public void Cancel(EnrollmentContext context)
        {
            context.TransitionTo(new CancelledState());
        }

        public string GetStateName() => "Validating Payment";
    }
}
