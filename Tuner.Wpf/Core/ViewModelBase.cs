using Ninject;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    public abstract class ViewModelBase<TView> : NotificationBase, IViewModel<TView> where TView : IView
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

        public bool? ShowChild<T>(IViewModel<T> viewModel) where T : IDialogView
        {
            var view = viewModel.View;
            view.DataContext = viewModel;
           return view.ShowDialog(View);
        }

    }
}

