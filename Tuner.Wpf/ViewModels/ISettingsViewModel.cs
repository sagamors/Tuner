using NAudio.CoreAudioApi;
using System;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public class SettingsChangedEventArgs : EventArgs
    {
        
    }

    interface ISettingsViewModel : IViewModel<ISettingsView>, ISettings 
    {
        uint SampleRate { set; get; }
        MMDevice SelectedDevice { set; get; }
    }
}
