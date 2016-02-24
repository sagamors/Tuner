using Ninject;
using System.Windows.Input;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    public abstract class DialogViewModelBase<TView> : ValidationViewModelBase<TView>, IDialogViewModelBase<TView> where TView : IDialogView
    {
        public DialogViewModelBase(IKernel container) : base(container)
        {

        }

        public void Show(object owner)
        {
            var view = View;
            view.DataContext = this;
            view.Show(owner);
        }

        public bool? ShowDialog(object owner)
        {
            var view = View;
            view.DataContext = this;
            return view.ShowDialog(owner);
        }
    }
}
