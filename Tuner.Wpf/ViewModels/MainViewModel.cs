using Ninject;
using System.Windows.Input;
using Tuner.Wpf.Core;
using Tuner.Wpf.Helpers;

namespace Tuner.Wpf.ViewModels
{
    class MainViewModel : ViewModelBase<IMainWindowView>, IMainWindowViewModel
    {
        public ICommand OpenSettingsCommand {private set; get; }

        public MainViewModel(IKernel kernel) : base(kernel)
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        public void OpenSettings()
        {
            ShowDialog(Container.Get<ISettingsViewModel>());
        }
    }
}
