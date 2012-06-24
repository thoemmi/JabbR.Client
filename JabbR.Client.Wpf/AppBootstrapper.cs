using Autofac;
using Caliburn.Micro.Autofac;
using JabbR.Client.Wpf.Shell;

namespace JabbR.Client.Wpf {
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel> {
        protected override void ConfigureBootstrapper() {
            base.ConfigureBootstrapper();
            AutoSubscribeEventAggegatorHandlers = true;
            ViewModelBaseType = typeof(IShell);
        }

        protected override void ConfigureContainer(ContainerBuilder builder) {
            base.ConfigureContainer(builder);
            builder.RegisterAssemblyTypes(this.GetType().Assembly).Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}