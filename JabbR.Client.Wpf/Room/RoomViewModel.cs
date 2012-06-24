using System;
using System.Linq;
using Caliburn.Micro;
using JabbR.Client.Wpf.Events;

namespace JabbR.Client.Wpf.Room {
    public class RoomViewModel : Screen, IHandle<RoomUpdatedEvent> {
        private string _messages;

        public void Handle(RoomUpdatedEvent message) {
            var room = message.Room;
            Messages = String.Join(Environment.NewLine, room.RecentMessages.Select(msg => msg.Content));
        }

        protected string Messages {
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