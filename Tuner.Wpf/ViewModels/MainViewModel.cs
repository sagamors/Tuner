using Ninject;
using System.Windows.Input;
using Tuner.Core;
using Tuner.Wpf.Core;
using Tuner.Wpf.Helpers;
using Tuner.Wpf.Sound;

namespace Tuner.Wpf.ViewModels
{
    class MainViewModel : DialogViewModelBase<IMainWindowView>, IMainWindowViewModel
    {
        public ISettingsViewModel Settings { private set; get; }
        public ICommand OpenSettingsCommand {private set; get; }
        public INoteCapture NoteCapture { set; get; }
        public INote TargetNote { set; get; }
        public INote CurrentNote { set; get; }
        public double CurrentFrequency { set; get; }

        public MainViewModel(IKernel kernel, ISettingsViewModel settings, INoteCapture noteCapture) : base(kernel)
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            Settings = settings;
            Settings.SettingsChanged += SettingsChanged;
            NoteCapture = noteCapture;
            NoteCapture.NoteDetected += NoteCapture_NoteDetected;
        }

        public void ValidateChildren()
        {
            if (Settings.Validate()) return;
            OpenSettings();
            if (Settings.IsValid) return;
            View.Close();
        }

        public void OpenSettings()
        {
            Show<ISettingsView>(Settings);
        }

        private void SettingsChanged(object sender, System.EventArgs e)
        {
            NoteCapture.Stop();
            NoteCapture.SampleRate = Settings.SampleRate;
            NoteCapture.Device = Settings.SelectedDevice;
            NoteCapture.Start();
        }

        private void NoteCapture_NoteDetected(object sender, NoteDetectedEvent e)
        {
            
        }
    }
}
