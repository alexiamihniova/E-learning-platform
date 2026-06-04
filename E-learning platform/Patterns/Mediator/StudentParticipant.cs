namespace E_learning_platform.Patterns.Mediator
{
    public class StudentParticipant : Participant
    {
        public StudentParticipant(string name, ILearningMediator mediator) 
            : base(name, "Student", mediator) { }

        public override void Receive(string from, string message)
        {
            Messages.Add($"[Student {Name} received from {from}]: {message}");
        }
    }
}
