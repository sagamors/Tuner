using System;
using System.Collections.ObjectModel;
using NAudio.CoreAudioApi;
using Ninject;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public class SettingsViewModel : ViewModelBase<ISettingsView>, ISettingsViewModel
    {
        public int BitDepth { set; get; }
        public int ChannelCount { set; get; }
        public int SampleRate { set; get; }
        public ObservableCollection<MMDevice> Devices { set; get; }
        public MMDevice SelectedDevice   { set; get; }
        public SettingsViewModel(IKernel kernel) : base(kernel)
        { 

        }
    }
}
