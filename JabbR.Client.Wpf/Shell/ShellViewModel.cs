using System;
using Caliburn.Micro;
using JabbR.Client.Models;
using JabbR.Client.Wpf.Events;
using JabbR.Client.Wpf.Mdi;
using JabbR.Client.Wpf.Room;
using JabbR.Client.Wpf.TitleBar;
using Message = JabbR.Client.Models.Message;

namespace JabbR.Client.Wpf.Shell {
    public class ShellViewModel : Conductor<IScreen> {
        private readonly TitleBarViewModel _titleBar;
        private readonly MdiViewModel _mdi;
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<RoomViewModel> _roomViewModelCreator;
        private JabbRClient _client;

        private const string SERVER = "http://jabbr-staging.apphb.com/";
        private const string USERNAME = "testclient";
        private const string PASSWORD = "password";

        public ShellViewModel(TitleBarViewModel titleBar, MdiViewModel mdi, IEventAggregator eventAggregator, Func<RoomViewModel> roomViewModelCreator) {
            _titleBar = titleBar;
            _mdi = mdi;
            _eventAggregator = eventAggregator;
            _roomViewModelCreator = roomViewModelCreator;

            base.DisplayName = "Jabbr Client";

            base.ActivateItem(mdi);
        }

        protected override void OnInitialize() {
            base.OnInitialize();

            _client = new JabbRClient(SERVER);
            _client.Connect(USERNAME, PASSWORD).ContinueWith(task => OnAfterConnect(task.Result));
            _client.MessageReceived += ClientOnMessageReceived;
        }

        private void ClientOnMessageReceived(Message message, string room) {
            _eventAggregator.Publish(new MessageReceivedEvent(room, message));
        }

        private void OnAfterConnect(LogOnInfo info) {
            foreach (var room in info.Rooms) {
                Console.WriteLine(room.Name);
                Console.WriteLine(room.Private);

                var roomView = _roomViewModelCreator();
                roomView.SetName(room.Name);
                _mdi.Open(roomView);

                _client.GetRoomInfo(room.Name).ContinueWith(task => OnAfterGetRoomInfo(task.Result));
            }
        }

        private void OnAfterGetRoomInfo(Models.Room room) {
            _eventAggregator.Publish(new RoomUpdatedEvent(room));
        }

        public TitleBarViewModel TitleBar {
            get { return _titleBar; }
        }

        public MdiViewModel Mdi {
            get { return _mdi; }
        }
    }
}

