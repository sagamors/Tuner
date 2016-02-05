using System;

namespace Tuner.Core
{
    public class NoteFinder : INoteFinder
    {

        const uint BASE_NOTE_OCTAVE = 4;
        const int OFFSET_TO_BEGIN_OCTAVE = 9;
        const double CONST = 1.0594;
        public INoteFactory NoteFactory {private set; get; }

        public NoteFinder(INoteFactory noteFactory)
        {
            NoteFactory = noteFactory;
        }

        public INote FindNearestNote(double frequency)
        {
            var numberOfHalfSteps = Round( Math.Log(frequency / NoteFactory.MainFrequnect, CONST));
            var indexFromZeroOctave = (BASE_NOTE_OCTAVE * 12 + OFFSET_TO_BEGIN_OCTAVE + numberOfHalfSteps);
            var indexNote =(uint) Round(indexFromZeroOctave % 12);
            var octave = (uint)Round(indexFromZeroOctave / 12);
            var concreteFrequency = NoteFactory.MainFrequnect * Math.Pow(CONST, numberOfHalfSteps);
            return NoteFactory.CreateNote(indexNote, octave, concreteFrequency);
        }

        private double Round(double value)
        {
            return Math.Round(value);
        }
    }
}
