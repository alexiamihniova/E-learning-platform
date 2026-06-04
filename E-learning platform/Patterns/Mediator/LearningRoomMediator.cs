using System.Collections.Generic;

namespace E_learning_platform.Patterns.Mediator
{
    public class LearningRoomMediator : ILearningMediator
    {
        private readonly List<Participant> _participants = new List<Participant>();
        public List<string> MessageLog { get; } = new List<string>();

        public void RegisterParticipant(Participant participant)
        {
            if (!_participants.Contains(participant))
            {
                _participants.Add(participant);
            }
        }

        public void SendMessage(string message, Participant sender)
        {
            MessageLog.Add($"{sender.Role} {sender.Name}: {message}");
            
            foreach (var participant in _participants)
            {
                // Message is broadcast to all EXCEPT the sender
                if (participant != sender)
                {
                    participant.Receive(sender.Name, message);
                }
            }
        }
    }
}
