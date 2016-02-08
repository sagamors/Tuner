using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    public interface IDialogViewModelBase<TView> : IViewModel<TView> where TView : IDialogView
    {
        bool? ShowDialog(object owner);
        void Show(object owner);
    }

}
