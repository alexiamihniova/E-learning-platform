namespace E_learning_platform.Patterns.Mediator
{
    public abstract class Participant
    {
        public string Name { get; }
        public string Role { get; }
        protected ILearningMediator Mediator;
        public List<string> Messages { get; } = new List<string>();

        protected Participant(string name, string role, ILearningMediator mediator)
        {
            Name = name;
            Role = role;
            Mediator = mediator;
        }

        public virtual void Send(string message)
        {
            Mediator.SendMessage(message, this);
        }

        public abstract void Receive(string from, string message);
    }
}
