using Caliburn.Micro.Autofac;
using JabbR.Client.Wpf.Shell;

namespace JabbR.Client.Wpf {
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel> {
        protected override void ConfigureBootstrapper() {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
            AutoSubscribeEventAggegatorHandlers = true;
        }
    }
}