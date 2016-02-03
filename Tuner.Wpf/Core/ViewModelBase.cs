using PropertyChanged;
using Ninject;
using System;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    [ImplementPropertyChanged]
    public class ViewModelBase<TView> : IViewModel<TView> where TView : IView
    {
        public ViewModelBase(IKernel container)
        {
            Container = container;
            View.DataContext = this;
        }

        public IKernel Container { get; set; }

        public TView View
        {
            get
            {
                return Container.Get<TView>();
            }
        }

        public void ShowDialog<T>(IViewModel<T> viewModel)  where T : IDialogView
        {
            viewModel.View.ShowDialog(View);
        }
    }
}

