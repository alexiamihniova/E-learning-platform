using System;

namespace E_learning_platform.Patterns.AbstractFactory
{
    // Concrete Product A1
    // Why: Specific implementation of a Certificate for the "Standard" family.
    public class DigitalCertificate : ICertificate
    {
        public void Print()
        {
            Console.WriteLine("[Digital Certificate] Sending PDF via email...");
        }
    }
}
