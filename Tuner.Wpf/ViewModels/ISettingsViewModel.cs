using NAudio.CoreAudioApi;
using System;
using System.Collections.ObjectModel;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public enum eChannelType { Mono = 0x1, Stereo = 0x2 }
    public interface ISettingsViewModel : IDialogViewModelBase<ISettingsView>, IValidationViewModel
    {
        ReadOnlyObservableCollection<MMDevice> Devices { get; }
        MMDevice SelectedDevice { set; get; }
        uint SampleRate { set; get; }
        int BitDepth { set; get; }
        eChannelType ChannelType { set; get; }
        event EventHandler SettingsChanged;
    }
}
