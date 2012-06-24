namespace JabbR.Client.Wpf.Events {
    public class RoomUpdatedEvent {
        private readonly Models.Room _room;

        public RoomUpdatedEvent(Models.Room room) {
            _room = room;
        }

        public Models.Room Room {
            get { return _room; }
        }
    }
}