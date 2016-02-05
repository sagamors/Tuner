using Ninject;
using System.Windows.Input;
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

        public MainViewModel(IKernel kernel, ISettingsViewModel settings, INoteCapture noteCapture) : base(kernel)
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            Settings = settings;
            NoteCapture = noteCapture;
            Settings.SettingsChanged += SettingsChanged;
        }

        private void SettingsChanged(object sender, System.EventArgs e)
        {
            NoteCapture.Stop();
            NoteCapture.SampleRate = Settings.SampleRate;
            NoteCapture.Device = Settings.SelectedDevice;
            NoteCapture.Start();
        }

        public void OpenSettings()
        {
            ShowDialog(Settings);
        }
    }

    public  static class Ex
    {
        public static void Show(this ViewModelBase<ISettingsView> self)
        {
            
        }
    }
}
