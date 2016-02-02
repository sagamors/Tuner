using Ninject;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public class SettingsViewModel : ViewModelBase<ISettingsView>, ISettingsViewModel
    {
        public SettingsViewModel(ISettingsView window, IKernel kernel) : base(window,kernel)
        { 

        }
    }
}
