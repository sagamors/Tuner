using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    interface ISettingsViewModel : IViewModel<ISettingsView>
    {
        ObservableCollection<MMDevice> Devices { set; get; }
        MMDevice SelectedDevice { set; get; }
        int SampleRate { set; get; }
        int BitDepth { set; get; }
        int ChannelCount { set; get; }
    }
}
