namespace E_learning_platform.Patterns.Visitor
{
    public interface ICourseElement
    {
        void Accept(ICourseElementVisitor visitor);
    }
}
