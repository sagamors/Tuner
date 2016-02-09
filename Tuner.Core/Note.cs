using System;

namespace Tuner.Core
{ 
    public class Note : INote, IEquatable<Note>
    {
        public uint Index { get; } 
        private NoteFactory NoteFactory { get; } 
        private readonly double _tolerance = 0.01;
        public double Frequency { private set; get; }
        public string Name { private set; get; }
        public uint Octave {get;}
        public string FullName { get { return Name + Octave; } }

        public Note(string nameNote, uint octave, uint index, NoteFactory noteFactory)
        {
            Name = nameNote;
            NoteFactory = noteFactory;
            Octave = octave;
            Index = index;
            NoteFactory.MainFrequencyChanged += NoteFactory_MainFrequencyChanged;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (Object.ReferenceEquals(this,obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
                return false;

            var note = obj as Note;

            if (note == null)
                return false;

            return note.Equals(this);
        }

        public bool Equals(Note other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Math.Abs(Frequency - other.Frequency) > _tolerance ^ Name != other.Name ^ Octave != other.Octave;
        }

        public override int GetHashCode()
        {
            return Frequency.GetHashCode() ^ FullName.GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }

        private void NoteFactory_MainFrequencyChanged(object sender, MainFrequencyChangedEventArgs e)
        {
            RefreshFrequency();
        }

        private void RefreshFrequency()
        {
            Frequency = FrequencyUtils.GetFrequency(Index, Octave, NoteFactory.MainFrequency);
        }
    }
}
