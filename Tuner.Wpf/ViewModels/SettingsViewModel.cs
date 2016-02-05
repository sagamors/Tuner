using Ninject;
using Tuner.Wpf.Core;
using NAudio.CoreAudioApi;
using System.Windows.Input;
using Tuner.Wpf.Helpers;
using System;
using System.Collections.ObjectModel;

namespace Tuner.Wpf.ViewModels
{ 
    public class SettingsViewModel : DialogViewModelBase<ISettingsView>, ISettingsViewModel
    {
        public uint SampleRate { set; get; } = 44100;
        public MMDevice SelectedDevice { set; get; }
        public ObservableCollection<MMDevice> Devices { set; get; }

        private int _bit = 32;
        public int BitDepth
        {
            set
            {
                _bit = value;
            }
            get
            {
                return _bit;
            }
        }

        public eChannelType ChannelType { set; get; } = eChannelType.Mono;
        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        public ICommand OkCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        public event EventHandler SettingsChanged;

        public SettingsViewModel(IKernel kernel) : base(kernel)
        {
            OkCommand = new RelayCommand(Ok, (p) => IsValid);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Ok()
        {
            if (IsValid)
            {

            }
        }

        private void Cancel()
        {

        }

    }
}
