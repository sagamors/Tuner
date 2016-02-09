using System;

namespace Tuner.Core
{
    public class MainFrequencyChangedEventArgs : EventArgs
    {
        public double Frequency { get; }

        public MainFrequencyChangedEventArgs(double frequency)
        {
            Frequency = frequency;
        }
    }

    public class NoteFactory : INoteFactory
    {
        string[] _noteNames = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public double MainFrequency
        {
            set
            {
                _mainFrequency = value;
                OnMainFrequencyChanged();
            }
            get { return _mainFrequency; }
        }


        private double _mainFrequency;

        public NoteFactory()
        {
            MainFrequency = 440;
        }

        public INote CreateNote(uint indexNote, uint octave)
        {
            return new Note(_noteNames[indexNote], octave, indexNote, this);
        }

        public INote CreateNote(eNote note, uint octave)
        {
            return CreateNote((uint)note,  octave);
        }

        public event EventHandler<MainFrequencyChangedEventArgs> MainFrequencyChanged;

        private void OnMainFrequencyChanged()
        {
            MainFrequencyChanged?.Invoke(this, new MainFrequencyChangedEventArgs(MainFrequency));
        }
    }
}
