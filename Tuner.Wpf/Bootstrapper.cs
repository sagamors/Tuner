using Ninject;
using Tuner.Core;
using Tuner.Wpf.Core;
using Tuner.Wpf.Sound;
using Tuner.Wpf.ViewModels;
using Tuner.Wpf.Views;
using Tuner.Wpf.Windows;

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
            Container.Bind<IAddNewPresetView>().To<AddNewPresetWindow>();
            Container.Bind<IAddNewPresetViewModel>().To<AddNewPresetViewModel>();
            Container.Bind<INoteCapture>().To<NoteCapture>().InSingletonScope();
            Container.Bind<INoteFinder>().To<NoteFinder>().InSingletonScope();
            Container.Bind<INoteFactory>().To<NoteFactory>().InSingletonScope();
            Container.Bind<IInstrument>().To<SixStringGuitar>().InSingletonScope();
        }
    }
}
