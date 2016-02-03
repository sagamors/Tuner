using Ninject;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public class SettingsViewModel : ViewModelBase<ISettingsView>, ISettingsViewModel
    {
        public SettingsViewModel(IKernel kernel) : base(kernel)
        { 

        }
    }
}
