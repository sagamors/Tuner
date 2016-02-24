using Ninject;
using Tuner.Wpf.Core;
using NAudio.CoreAudioApi;
using System.Windows.Input;
using Tuner.Wpf.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Tuner.Wpf.Sound;

namespace Tuner.Wpf.ViewModels
{ 
    public class SettingsViewModel : DialogViewModelBase<ISettingsView>, ISettingsViewModel
    {
        private AudioDevicesWatcher _notificationClient;
        public uint SampleRate { set; get; } = 44100;
        [Required]
        public MMDevice SelectedDevice { set; get; }
        [HasElements(ErrorMessage = "Tuner was unable to find any usable recording device")]
        public ReadOnlyObservableCollection<MMDevice> Devices {private set; get; }
        public int BitDepth { set; get; } = 32;
        public eChannelType ChannelType { set; get; } = eChannelType.Mono;
        public ICommand OkCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        public SettingsViewModel(IKernel kernel) : base(kernel)
        {
            OkCommand = new RelayCommand(Ok, (p) => IsValid);
            CancelCommand = new RelayCommand(Cancel);
            _notificationClient = new AudioDevicesWatcher();
            Devices = new ReadOnlyObservableCollection<MMDevice>(_notificationClient.Devices);
            _notificationClient.DefaultDeviceChanged += NotificationClient_DefaultDeviceChanged;
            SelectedDevice = _notificationClient.DefaultDevice;
        }

        private void NotificationClient_DefaultDeviceChanged(object sender, DefaultDeviceChangedEventArgs e)
        {
            SelectedDevice = e.Device;
        }


        private void Ok()
        {
            if (IsValid)
            {
                OnSettingsChanged();
            }
        }

        private void Cancel()
        {

        }

        public event EventHandler SettingsChanged;

        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
