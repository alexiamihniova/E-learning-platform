namespace E_learning_platform.Interfaces
{
    /// <summary>
    /// Target Interface for Adapter Pattern.
    /// Defines a unified interface for processing payments, regardless of the underlying gateway.
    /// </summary>
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }
}
