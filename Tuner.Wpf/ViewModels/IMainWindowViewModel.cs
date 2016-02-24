using Tuner.Core;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    interface IMainWindowViewModel : IDialogViewModelBase<IMainWindowView>
    {
        INote TargetNote { set; get; }
        INote CurrentNote { set; get; }
        double CurrentFrequency { set; get; }
        bool Validate();
        void AcceptSettings();
    }
}
