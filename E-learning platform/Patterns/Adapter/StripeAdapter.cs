using E_learning_platform.Interfaces;
using System;

namespace E_learning_platform.Patterns.Adapter
{
    /// <summary>
    /// Adapter Class: Translates IPaymentProcessor calls to the specific StripeApi calls.
    /// Example of adapting a completely different return type and parameter type.
    /// </summary>
    public class StripeAdapter : IPaymentProcessor
    {
        private readonly StripeApi _stripeApi;

        public StripeAdapter(StripeApi stripeApi)
        {
            _stripeApi = stripeApi ?? throw new ArgumentNullException(nameof(stripeApi));
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("[StripeAdapter] Adapting ProcessPayment to StripeApi.ChargePayment.");
            
            // Note the data type conversion double <-> decimal
            string response = _stripeApi.ChargePayment((double)amount);
            
            return response == "SUCCESS";
        }
    }
}
