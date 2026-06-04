namespace E_learning_platform.Patterns.AbstractFactory
{
    // Abstract Factory
    // Why: Declares a set of methods for creating abstract products.
    // It groups related products (Certificate + Badge) without specifying their concrete classes.
    public interface IAwardFactory
    {
        ICertificate CreateCertificate();
        IBadge CreateBadge();
    }
}
