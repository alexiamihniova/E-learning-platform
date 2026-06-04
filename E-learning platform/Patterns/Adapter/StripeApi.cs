using System;

namespace E_learning_platform.Patterns.Adapter
{
    /// <summary>
    /// Adaptee Class: Represents another specific, incompatible third-party API (Stripe).
    /// </summary>
    public class StripeApi
    {
        public string ChargePayment(double totalAmount)
        {
            Console.WriteLine($"[Stripe API] Charging total amount of {totalAmount:C} via Stripe infrastructure.");
            // Simulate varying return types or signatures
            return totalAmount > 0 ? "SUCCESS" : "FAILURE";
        }
    }
}
