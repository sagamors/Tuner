using PropertyChanged;
using Ninject;
using Tuner.Wpf.ViewModels;
using System.Windows;

namespace Tuner.Wpf.Core
{
    [ImplementPropertyChanged]
    public abstract class ViewModelBase<TView> : IViewModel<TView> where TView : IView
    {
        public ViewModelBase(IKernel container)
        {
            Container = container;
        }

        public IKernel Container { get; private set; }

        public TView View
        {
            get
            {
                return Container.Get<TView>();
            }
        }

        public bool? ShowDialog<T>(IViewModel<T> viewModel)  where T : IDialogView
        {
            var view = viewModel.View;
            view.DataContext = viewModel;
            return view.ShowDialog(View);
        }
    }

    public interface IDialogViewModelBase<TView> : IViewModel<TView> where TView : IDialogView
    {
        bool? ShowThisDialog(object owner);
    }

    [ImplementPropertyChanged]
    public abstract class DialogViewModelBase<TView> : ViewModelBase<TView>, IDialogViewModelBase<TView> where TView : IDialogView
    {

        public DialogViewModelBase(IKernel container) : base(container)
        {

        }

        public bool? ShowThisDialog(object owner)
        {
            var view = View;
            view.DataContext = this;
            return view.ShowDialog(owner);
        }
    }
}

