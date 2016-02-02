using PropertyChanged;
using Ninject;

namespace Tuner.Wpf.Core
{
    [ImplementPropertyChanged]
    public class ViewModelBase<TView> : IViewModel<TView> where TView : IView
    {
        public ViewModelBase(TView view, IKernel container)
        {
            View = view;
            View.DataContext = this;
            Container = container;
        }

        public IKernel Container { get; set; }
        public TView View { get; set; }
    }
}

