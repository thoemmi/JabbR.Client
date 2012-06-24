using Autofac;
using Caliburn.Micro.Autofac;
using JabbR.Client.Wpf.Shell;

namespace JabbR.Client.Wpf {
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel> {
        protected override void ConfigureBootstrapper() {
            //  you must call the base version first!
            base.ConfigureBootstrapper();
            //  override namespace naming convention
            EnforceNamespaceConvention = false;
            //  auto subsubscribe event aggregators
            AutoSubscribeEventAggegatorHandlers = true;

            ViewModelBaseType = typeof(IShell);
        }

        protected override void ConfigureContainer(ContainerBuilder builder) {
            base.ConfigureContainer(builder);
            builder.RegisterAssemblyTypes(this.GetType().Assembly).Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}