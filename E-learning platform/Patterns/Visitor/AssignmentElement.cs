namespace E_learning_platform.Patterns.Visitor
{
    public class AssignmentElement : ICourseElement
    {
        public string Title { get; }
        public decimal PointsValue { get; }

        public AssignmentElement(string title, decimal pointsValue)
        {
            Title = title;
            PointsValue = pointsValue;
        }

        public void Accept(ICourseElementVisitor visitor)
        {
            visitor.VisitAssignment(this);
        }
    }
}
