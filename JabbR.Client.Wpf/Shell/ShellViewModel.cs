using System.ComponentModel.Composition;
using Caliburn.Micro;
using JabbR.Client.Wpf.TitleBar;

namespace JabbR.Client.Wpf.Shell {
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell {
        private readonly TitleBarViewModel _titleBar;

        [ImportingConstructor]
        public ShellViewModel(TitleBarViewModel titleBar) {
            _titleBar = titleBar;

            base.DisplayName = "Jabbr Client";
        }

        public TitleBarViewModel TitleBar {
            get { return _titleBar; }
        }
    }
}

