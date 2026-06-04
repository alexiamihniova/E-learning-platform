using E_learning_platform.Interfaces;
using System;

namespace E_learning_platform.Patterns.Adapter
{
    /// <summary>
    /// Adapter Class: Translates IPaymentProcessor calls to the specific PayPalApi calls.
    /// Uses Object Adapter strategy (composition).
    /// </summary>
    public class PayPalAdapter : IPaymentProcessor
    {
        private readonly PayPalApi _payPalApi;

        public PayPalAdapter(PayPalApi payPalApi)
        {
            _payPalApi = payPalApi ?? throw new ArgumentNullException(nameof(payPalApi));
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("[PayPalAdapter] Adapting ProcessPayment to PayPalApi.MakePayment.");
            return _payPalApi.MakePayment(amount);
        }
    }
}
