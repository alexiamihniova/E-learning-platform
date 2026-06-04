using System.Collections.Generic;

namespace E_learning_platform.Patterns.State
{
    public class EnrollmentContext
    {
        private IEnrollmentState _state;
        public decimal TotalPaid { get; private set; }
        public decimal RequiredAmount { get; }
        public List<string> TransitionLog { get; } = new List<string>();

        public EnrollmentContext(decimal requiredAmount)
        {
            RequiredAmount = requiredAmount;
            _state = new WaitingForPaymentState();
            LogTransition("Initial state: Waiting for Payment");
        }

        public void TransitionTo(IEnrollmentState state)
        {
            _state = state;
            LogTransition("Current state: " + state.GetStateName());
        }

        public void AddPayment(decimal amount)
        {
            TotalPaid += amount;
            _state.AddPayment(this, amount);
        }

        public void ValidatePayment()
        {
            _state.ValidatePayment(this);
        }

        public void CompleteEnrollment()
        {
            _state.CompleteEnrollment(this);
        }

        public void Cancel()
        {
            _state.Cancel(this);
        }

        public string GetCurrentStateName() => _state.GetStateName();

        private void LogTransition(string message)
        {
            TransitionLog.Add(message);
        }
    }
}
