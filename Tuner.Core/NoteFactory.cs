using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Core
{
    public class NoteFactory : INoteFactory
    {
        public double MainFrequnect { set; get; }
        string[] noteNames = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        public NoteFactory()
        {
            MainFrequnect = 440;
        }

        public INote CreateNote(uint indexNote, uint octave, double frequency)
        {
            return new Note(noteNames[indexNote], octave, frequency);
        }
    }
}
