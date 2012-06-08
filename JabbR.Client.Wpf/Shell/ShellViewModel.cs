using System.ComponentModel.Composition;

namespace JabbR.Client.Wpf.Shell {
    [Export(typeof(IShell))]
    public class ShellViewModel : IShell {}
}

