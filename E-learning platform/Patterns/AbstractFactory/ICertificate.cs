namespace E_learning_platform.Patterns.AbstractFactory
{
    // Abstract Product A
    // Why: Defines the interface for one family of products (Certificates).
    // The factory will produce concrete versions of this product.
    public interface ICertificate
    {
        void Print();
    }
}
