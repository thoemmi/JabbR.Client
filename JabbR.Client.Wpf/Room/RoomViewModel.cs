using System;
using System.Linq;
using Caliburn.Micro;
using JabbR.Client.Wpf.Events;

namespace JabbR.Client.Wpf.Room {
    public class RoomViewModel : Screen, IHandle<RoomUpdatedEvent>, IHandle<MessageReceivedEvent> {
        private string _messages = "hello";
        private string _name = "name";

        public void SetName(string name) {
            _name = name;
            DisplayName = name;
            Messages = name;
        }

        public void Handle(RoomUpdatedEvent @event) {
            var room = @event.Room;
            if (room.Name != _name) {
                return;
            }

            Messages = String.Join(Environment.NewLine, room.RecentMessages.Select(msg => msg.Content));
        }

        public void Handle(MessageReceivedEvent @event) {
            if (@event.Room != _name) {
                return;
            }

            Messages += Environment.NewLine + String.Format("{0}: {1}", @event.Message.User, @event.Message.Content);
        }

        public string Messages {
            get { return _messages; }
            set {
                if (_messages != value) {
                    _messages = value;
                    NotifyOfPropertyChange(() => Messages);
                }
            }
        }
    }
}