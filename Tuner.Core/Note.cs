using System;

namespace Tuner.Core
{
    public class Note : INote, IEquatable<Note>
    {
        public double Frequency { private set; get; }
        public string Name {private set; get; }
        public uint Octave { private set; get; }
        public string FullName { get { return Name + Octave; } }

        public Note(string nameNote,uint octave, double frequency)
        {
            Name = nameNote;
            Frequency = frequency;
            Octave = octave;
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

            return Frequency != other.Frequency ^ Name != other.Name ^ Octave != other.Octave;
        }

        public override int GetHashCode()
        {
            return Frequency.GetHashCode() ^ FullName.GetHashCode();
        }
    }
}
