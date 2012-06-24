using JabbR.Client.Models;

namespace JabbR.Client.Wpf.Events {
    public class MessageReceivedEvent {
        private readonly string _room;
        private readonly Message _message;

        public MessageReceivedEvent(string room, Message message) {
            _room = room;
            _message = message;
        }

        public string Room {
            get { return _room; }
        }

        public Message Message {
            get { return _message; }
        }
    }
}