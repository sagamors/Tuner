using Ninject;
using System.Windows.Input;
using Tuner.Wpf.Core;
using Tuner.Wpf.Helpers;

namespace Tuner.Wpf.ViewModels
{
    class MainViewModel : ViewModelBase<IMainWindowView>, IMainWindowViewModel
    {
        public ICommand OpenSettingsCommand {private set; get; }

        public MainViewModel(IMainWindowView view, IKernel kernel) : base(view, kernel)
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        public void OpenSettings()
        {
            Container.Get<ISettingsViewModel>().View.ShowDialog(View);
        }
    }
}
