namespace E_learning_platform.Patterns.Mediator
{
    public interface ILearningMediator
    {
        void SendMessage(string message, Participant sender);
        void RegisterParticipant(Participant participant);
    }
}
