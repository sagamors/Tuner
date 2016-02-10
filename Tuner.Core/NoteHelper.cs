using System;

namespace Tuner.Core
{
    public class NoteHelper
    {
        const int BASE_NOTE_OCTAVE = 4;
        const int OFFSET_TO_BEGIN_OCTAVE = 9;
        const double CONST = 1.0594;
        const uint NUMBER_NOTE = 12;

        public static uint GetTotalIndexNote(double frequency, double mainFrequency)
        {
            var numberOfHalfSteps = Round(Math.Log(frequency / mainFrequency, CONST));
            uint indexFromZeroOctave = (uint) (BASE_NOTE_OCTAVE * 12 + OFFSET_TO_BEGIN_OCTAVE + numberOfHalfSteps);
            return indexFromZeroOctave;
        }

        public static double GetFrequency(uint index,uint octave, double mainFrequency)
        {
            return mainFrequency* Math.Pow(CONST, ((int)octave*NUMBER_NOTE + index) - (BASE_NOTE_OCTAVE * NUMBER_NOTE + OFFSET_TO_BEGIN_OCTAVE));
        }

        public static uint GetOctave(uint totalIndex)
        {
            return totalIndex / NUMBER_NOTE;
        }

        public static uint GetIndex(uint totalIndex)
        {
            return totalIndex % NUMBER_NOTE;
        }

        private static double Round(double value)
        {
            return Math.Round(value);
        }
    }
}