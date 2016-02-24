using System.Collections.ObjectModel;
using Tuner.Wpf.Core;
using Tuner.Wpf.Sound;

namespace Tuner.Wpf.ViewModels
{
    interface IAddNewPresetViewModel : IDialogViewModelBase<IAddNewPresetView>
    {
        IPreset Preset { set; get; }
        IInstrument Instrument { set; get; }
    }
}