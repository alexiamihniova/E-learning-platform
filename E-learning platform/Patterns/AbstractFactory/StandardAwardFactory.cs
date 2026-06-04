namespace E_learning_platform.Patterns.AbstractFactory
{
    public class StandardAwardFactory : IAwardFactory
    {
        // REFACTORING 2: Expression-bodied members
        public ICertificate CreateCertificate() => new DigitalCertificate();

        public IBadge CreateBadge() => new BronzeBadge();
    }
}
