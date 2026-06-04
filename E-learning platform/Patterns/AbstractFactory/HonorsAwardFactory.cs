namespace E_learning_platform.Patterns.AbstractFactory
{
    public class HonorsAwardFactory : IAwardFactory
    {
        // REFACTORING 2: Expression-bodied members
        public ICertificate CreateCertificate() => new PhysicalCertificate();

        public IBadge CreateBadge() => new GoldBadge();
    }
}
