using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
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
        private IInstrument _selectedInstrument;
        public ISettingsViewModel Settings { private set; get; }
        public INoteCapture NoteCapture { set; get; }
        public INote TargetNote { set; get; }
        public INote CurrentNote { set; get; }
        public double CurrentFrequency { set; get; }
        public ICommand OpenSettingsCommand { private set; get; }
        public ICommand CloseCommand { private set; get; }
        public ICommand DeleteFromFavoriteCommand { private set; get; }
        public IInstrument SelectedInstrument
        {
            set
            {
                _selectedInstrument = value;
                if (_selectedInstrument != null)
                {
                    var collectionViewSource = new CollectionViewSource { Source = _selectedInstrument.Presets };
                    collectionViewSource.LiveFilteringProperties.Add("IsFavorite");
                    collectionViewSource.IsLiveFilteringRequested = true;
                    FavoritePresets = collectionViewSource.View;
                    FavoritePresets.Filter = o =>
                    {
                        var preset = o as IPreset;
                        return preset?.IsFavorite ?? false;
                    };
                    FavoritePresets.CollectionChanged += (sender, args) =>
                    {
                        SetFirstInPresetsWhenFavoriteEmpty();
                    };
                }
            }
            get { return _selectedInstrument; }
        }

        public ICollectionView FavoritePresets { private set; get; }
        public ICommand DeletePresetCommand { private set; get; }
        public ICommand AddNewPresetDialogShowCommand { private set; get; }
        public ICommand EditPresetDialogShowCommand { private set; get; }
        public MainViewModel(IKernel kernel, IInstrument selectedInstrument, ISettingsViewModel settings, INoteCapture noteCapture) : base(kernel)
        {
            OpenSettingsCommand = new RelayCommand(SettingsShow);
            CloseCommand = new RelayCommand(Close);
            DeleteFromFavoriteCommand = new RelayCommand(Delete, o => o is IPreset);
            AddNewPresetDialogShowCommand = new RelayCommand(AddNewPresetDialogShow);
            EditPresetDialogShowCommand = new RelayCommand((p)=> { EditPresetDialogShow(p as IPreset); });
            Settings = settings;
            Settings.SettingsChanged += SettingsChanged;
            NoteCapture = noteCapture;
            NoteCapture.NoteDetected += NoteCapture_NoteDetected;
            SelectedInstrument = selectedInstrument;
            Application.Current.Exit += Current_Exit;
        }

        private void Delete(object o)
        {
            var preset = o as IPreset;
            if(preset==null) return;
            preset.IsFavorite = false;
            FavoritePresets.Refresh();
            SetFirstInPresetsWhenFavoriteEmpty();
        }

        private void SetFirstInPresetsWhenFavoriteEmpty()
        {
            if (FavoritePresets.IsEmpty)
            {
                SelectedInstrument.SelectedPreset = SelectedInstrument.Presets.FirstOrDefault();
            }
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            Close();
        }

        public bool Validate()
        {
            if (Settings.IsValid) return true;
            if(Settings.Devices!=null || Settings.Devices.Count == 0)
            {
                MessageBox.Show(Settings.GetStringErrors(nameof(Settings.Devices)), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            SettingsShow();
            return Settings.IsValid;
        }

        public void SettingsShow()
        {
            ShowChild(Settings);
        }

        public void AddNewPresetDialogShow()
        {
            var preset = new Preset();
            EditPresetDialogShow(preset);
        }

        public void EditPresetDialogShow(IPreset preset)
        {
            var newPreset = Container.Get<IAddNewPresetViewModel>();
            // todo is not very good
            newPreset.Preset = (IPreset)preset.Clone();
            newPreset.Instrument = SelectedInstrument;
            if (ShowChild(newPreset) == true)
            {
                preset.CloneTo(preset);
            }
        }


        private void SettingsChanged(object sender, System.EventArgs e)
        {
            AcceptSettings();
        }

        private void NoteCapture_NoteDetected(object sender, NoteDetectedEvent e)
        {
            CurrentFrequency = e.Frequency;
            CurrentNote = e.NearestNote;
        }

        private void Close()
        {
            NoteCapture.Stop();
        }

        public void AcceptSettings()
        {
            NoteCapture.Stop();
            NoteCapture.SampleRate = Settings.SampleRate;
            NoteCapture.Device = Settings.SelectedDevice;
            NoteCapture.Start();
        }
    }
}
