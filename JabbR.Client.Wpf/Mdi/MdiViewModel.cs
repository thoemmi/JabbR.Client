using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace JabbR.Client.Wpf.Mdi {
    [Export]
    public class MdiViewModel : Conductor<IScreen>.Collection.OneActive {
        public void Open(IScreen screen) {
            ActivateItem(screen);
        }
    }
}