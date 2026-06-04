using System;

namespace E_learning_platform.Patterns.AbstractFactory
{
    // Concrete Product B2
    // Why: Specific implementation of a Badge for the "Honors" family.
    public class GoldBadge : IBadge
    {
        public void Wear()
        {
            Console.WriteLine("[Gold Badge] Shipped physically with a pin!");
        }
    }
}
