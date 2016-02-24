using NAudio.CoreAudioApi.Interfaces;
using System;
using System.Linq;
using NAudio.CoreAudioApi;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace Tuner.Wpf.Sound
{
    class AudioDevicesWatcher : IMMNotificationClient
    {
        private const DataFlow DataFlow = NAudio.CoreAudioApi.DataFlow.Capture;
        private const Role Role = NAudio.CoreAudioApi.Role.Multimedia;
        private const DeviceState DeviceState = NAudio.CoreAudioApi.DeviceState.Active;
        private readonly MMDeviceEnumerator _deviceEnum = new MMDeviceEnumerator();
        private readonly Dispatcher _dispatcher;

        public ObservableCollection<MMDevice> Devices { get; }
        public MMDevice DefaultDevice { private set; get; }

        public AudioDevicesWatcher()
        {
            try {
                _dispatcher = Dispatcher.CurrentDispatcher;
                Devices = new ObservableCollection<MMDevice>();
                _deviceEnum.RegisterEndpointNotificationCallback(this);
                RefreshDevices();
                RefreshDefaultDevice();
            }
            catch
            {

            }
        }

        void IMMNotificationClient.OnDefaultDeviceChanged(DataFlow dataFlow, Role deviceRole, string defaultDeviceId)
        {
            if (deviceRole == Role && dataFlow == DataFlow)
                OnDefaultDeviceChanged();
        }

        void IMMNotificationClient.OnDeviceAdded(string deviceId)
        {
            RefreshDevices();
        }

        void IMMNotificationClient.OnDeviceRemoved(string deviceId)
        {
            RefreshDevices();
        }

        void IMMNotificationClient.OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            RefreshDevices();
        }

        void IMMNotificationClient.OnPropertyValueChanged(string deviceId, PropertyKey propertyKey)
        {
            Debug.WriteLine("OnPropertyValueChanged:  formatId --> {0}  propertyId--> {1}", propertyKey.formatId.ToString(), propertyKey.propertyId.ToString());
        }

        private object _loker = new object();
        private void RefreshDevices()
        {
            lock (_loker)
            {
                _dispatcher.Invoke(() =>
                {
                    try
                    {
                        Devices.Clear();
                        foreach (var device in _deviceEnum.EnumerateAudioEndPoints(DataFlow, DeviceState))
                            Devices.Add(device);
                    }
                    catch (Exception ex)
                    {
                        Debug.Fail(ex.ToString());
                    }
                });
            }
        }

        public event EventHandler<DefaultDeviceChangedEventArgs> DefaultDeviceChanged;
        public void OnDefaultDeviceChanged()
        {
            RefreshDefaultDevice();
            DefaultDeviceChanged?.Invoke(this, new DefaultDeviceChangedEventArgs(DefaultDevice));
        }

        public void RefreshDefaultDevice()
        {
            lock (_loker)
            {
                _dispatcher.Invoke(() =>
                {
                    try
                    {
                        var device = _deviceEnum.GetDefaultAudioEndpoint(DataFlow, Role);
                        DefaultDevice = Devices.FirstOrDefault((d) => d.ID == device.ID);
                        if (DefaultDevice == null && Devices.Count > 0)
                            DefaultDevice = Devices.First();
                    }
                    catch (Exception ex)
                    {
                        Debug.Fail(ex.ToString());
                    }
                });
            }
        }
    }

    public class DefaultDeviceChangedEventArgs : EventArgs
    {
        public MMDevice Device { private set; get; }
        public DefaultDeviceChangedEventArgs(MMDevice device)
        {
            Device = device;
        }
    }
}
