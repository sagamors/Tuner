using Ninject;
using Tuner.Wpf.Core;
using Tuner.Wpf.ViewModels;
using Tuner.Wpf.Views;

namespace Tuner.Wpf
{
    public static class Bootstrapper
    {
        public static IKernel Container { private set; get; }

        public static void Initialize()
        {
            Container = new StandardKernel();
            Container.Bind<IMainWindowView>().To<MainWindow>().InSingletonScope();
            Container.Bind<IMainWindowViewModel>().To<MainViewModel>().InSingletonScope();
            Container.Bind<ISettingsView>().To<SettingsWindow>();
            Container.Bind<ISettingsViewModel>().To<SettingsViewModel>().InSingletonScope();  
        }
    }

    public interface IH
    {

    }

    public class h2 : WindowDialogBase, IH
    {
        public IKernel Kernel;
        public h2(IKernel kernel)
        {
            Kernel = kernel;
        }
    }
}
