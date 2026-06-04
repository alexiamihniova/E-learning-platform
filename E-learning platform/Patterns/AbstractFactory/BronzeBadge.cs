using System;

namespace E_learning_platform.Patterns.AbstractFactory
{
    // Concrete Product B1
    // Why: Specific implementation of a Badge for the "Standard" family.
    public class BronzeBadge : IBadge
    {
        public void Wear()
        {
            Console.WriteLine("[Bronze Badge] Added to user profile.");
        }
    }
}
