namespace E_learning_platform.Patterns.AbstractFactory
{
    // Abstract Product B
    // Why: Defines the interface for another family of products (Badges).
    // This allows creating related objects (Award = Certificate + Badge) coherently.
    public interface IBadge
    {
        void Wear();
    }
}
