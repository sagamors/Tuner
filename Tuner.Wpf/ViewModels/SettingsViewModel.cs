using System;
using Ninject;
using Tuner.Wpf.Core;
using NAudio.CoreAudioApi;
using System.Windows.Input;
using Tuner.Wpf.Helpers;

namespace Tuner.Wpf.ViewModels
{
    public class SettingsViewModel : ViewModelBase<ISettingsView>, ISettingsViewModel
    {
        public uint SampleRate { set; get; }
        public MMDevice SelectedDevice { set; get; }
        public ICommand OkCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        public event EventHandler SettingsChanged;

        public SettingsViewModel(IKernel kernel) : base(kernel)
        {
            OkCommand = new RelayCommand((p) => Ok(), (p)=>IsValid);
            CancelCommand = new RelayCommand((p) => Ok());
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
