using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;
using Tuner.Wpf.Core;
using Tuner.Wpf.Helpers;
using Tuner.Wpf.Sound;

namespace Tuner.Wpf.ViewModels
{
    class AddNewPresetViewModel : DialogViewModelBase<IAddNewPresetView>, IAddNewPresetViewModel
    {
        public IPreset Preset { set; get; }

        public ICommand OkCommand { set; get; }

        public IInstrument Instrument  { set; get; }

        public AddNewPresetViewModel(IKernel container) : base(container)
        {
            OkCommand = new RelayCommand(Ok);
        }

        private void Ok(object o)
        {

        }
    }
}
