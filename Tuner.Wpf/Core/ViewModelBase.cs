using PropertyChanged;
using Ninject;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    [ImplementPropertyChanged]
    public abstract class ViewModelBase<TView> : IViewModel<TView> where TView : IView
    {
        public ViewModelBase(IKernel container)
        {
            Container = container;
            View.DataContext = this;
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
            // todo: not set dataContext
            viewModel.View.DataContext = this;
            return viewModel.View.ShowDialog(View);
        }
    }
}

