using System;

namespace E_learning_platform.Patterns.AbstractFactory
{
    // Concrete Product A2
    // Why: Specific implementation of a Certificate for the "Honors" family.
    public class PhysicalCertificate : ICertificate
    {
        public void Print()
        {
            Console.WriteLine("[Physical Certificate] Printing on high-quality paper and mailing...");
        }
    }
}
