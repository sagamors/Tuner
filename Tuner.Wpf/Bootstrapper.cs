using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
}
