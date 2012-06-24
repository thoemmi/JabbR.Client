using System;
using Caliburn.Micro;
using JabbR.Client.Models;
using JabbR.Client.Wpf.Mdi;
using JabbR.Client.Wpf.Room;
using JabbR.Client.Wpf.TitleBar;

namespace JabbR.Client.Wpf.Shell {
    public class ShellViewModel : Conductor<IScreen>, IShell {
        private readonly TitleBarViewModel _titleBar;
        private readonly MdiViewModel _mdi;
        private JabbRClient _client;

        private const string SERVER = "http://jabbr-staging.apphb.com/";
        private const string USERNAME = "testclient";
        private const string PASSWORD = "password";

        public ShellViewModel(TitleBarViewModel titleBar, MdiViewModel mdi) {
            _titleBar = titleBar;
            _mdi = mdi;

            base.DisplayName = "Jabbr Client";

            base.ActivateItem(mdi);
        }

        protected override void OnInitialize() {
            base.OnInitialize();

            _client = new JabbRClient(SERVER);
            _client.Connect(USERNAME, PASSWORD).ContinueWith(task => OnAfterConnect(task.Result));
        }

        private void OnAfterConnect(LogOnInfo info) {
            var userId = info.UserId;
            foreach (var room in info.Rooms) {
                Console.WriteLine(room.Name);
                Console.WriteLine(room.Private);

                //_client.GetRoomInfo(room.Name).ContinueWith(task => OnAfterGetRoomInfo(task.Result));
                var roomView = new RoomViewModel();
                roomView.DisplayName = room.Name;
                _mdi.Open(roomView);
            }
        }

        //private void OnAfterGetRoomInfo(Room room) {
        //    //Console.WriteLine(room.RecentMessages);
        //}

        public TitleBarViewModel TitleBar {
            get { return _titleBar; }
        }

        public MdiViewModel Mdi {
            get { return _mdi; }
        }
    }
}

