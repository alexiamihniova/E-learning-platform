namespace E_learning_platform.Patterns.Mediator
{
    public class InstructorParticipant : Participant
    {
        public InstructorParticipant(string name, ILearningMediator mediator) 
            : base(name, "Instructor", mediator) { }

        public override void Receive(string from, string message)
        {
            Messages.Add($"[Instructor {Name} received from {from}]: {message}");
        }
    }
}
