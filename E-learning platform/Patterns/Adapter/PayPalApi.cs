using System;

namespace E_learning_platform.Patterns.Adapter
{
    /// <summary>
    /// Adaptee Class: Represents a specific, incompatible third-party API (PayPal).
    /// </summary>
    public class PayPalApi
    {
        public bool MakePayment(decimal sum)
        {
            Console.WriteLine($"[PayPal API] Processing payment of {sum:C} via PayPal infrastructure.");
            // Simulate payment processing logic
            return sum > 0;
        }
    }
}
