using System;
using System.Linq;
using Caliburn.Micro;
using JabbR.Client.Wpf.Events;
using Message = JabbR.Client.Models.Message;

namespace JabbR.Client.Wpf.Room {
    public class RoomViewModel : Screen, IHandle<RoomUpdatedEvent>, IHandle<MessageReceivedEvent> {
        private string _messages;
        private string _name;
        private string _topic;
        private int _unreadMessageCount;

        public void SetName(string name) {
            Name = name;
            DisplayName = name;
            Messages = name;
        }

        public void Handle(RoomUpdatedEvent @event) {
            var room = @event.Room;
            if (room.Name != _name) {
                return;
            }

            Topic = !String.IsNullOrWhiteSpace(room.Topic) ? room.Topic : room.Name;
            Messages = String.Join(Environment.NewLine, room.RecentMessages.Select(FormatMessage));
        }

        public void Handle(MessageReceivedEvent @event) {
            if (@event.Room != _name) {
                return;
            }

            Messages += Environment.NewLine + FormatMessage(@event.Message);
            if (!IsActive) {
                UnreadMessageCount++;
            }
        }

        private string FormatMessage(Message message) {
            return String.Format("{0}: {1}", message.User.Name, message.Content);
        }

        protected override void OnActivate() {
            base.OnActivate();
            UnreadMessageCount = 0;
        }

        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
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

        public int UnreadMessageCount {
            get { return _unreadMessageCount; }
            set {
                if (_unreadMessageCount != value) {
                    _unreadMessageCount = value;
                    NotifyOfPropertyChange(() => UnreadMessageCount);
                }
            }
        }

        public string Topic {
            get { return _topic; }
            set {
                if (_topic != value) {
                    _topic = value;
                    NotifyOfPropertyChange(() => Topic);
                }
            }
        }
    }
}