using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tuner.Core.Test
{
    [TestClass]
    public class NoteHelperTests
    {
        private double delta = 0.5;
        private double mainFrequency = 440;

        private eNote enote = eNote.G;
        private double frequency = 392;
        private uint octave = 4;
        private uint index = 7;
        private  uint totalIndex = 55;

        [TestMethod]
        public void NoteFinderTest()
        {
            NoteFactory noteFactory = new NoteFactory();
            NoteFinder noteFinder = new NoteFinder(noteFactory);
            INote note = noteFactory.CreateNote(enote, octave);
            var actual = noteFinder.FindNearestNote(110);
            Assert.AreEqual(actual, note);
        }

        [TestMethod]
        public void GetTotalIndexNoteTest()
        {
            var actualTotal = GetTotal();
            Assert.AreEqual(totalIndex, actualTotal );
        }

        [TestMethod]
        public void GetIndexNoteTest()
        {
            var actualIndex = GetIndex();
            Assert.AreEqual(index, actualIndex);
        }

        [TestMethod]
        public void GetOctaveNoteTest()
        {
            var actualOctave = GetOctave();
            Assert.AreEqual(octave, actualOctave);
        }

        [TestMethod]
        public void GetFrequencyTest()
        {
            var actualFreq = NoteHelper.GetFrequency(index, octave, mainFrequency);
            Assert.AreEqual(frequency, actualFreq, delta);
        }

        private uint GetTotal()
        {
             return NoteHelper.GetTotalIndexNote(frequency, mainFrequency);
        }

        private uint GetIndex()
        {
            var actualTotal = GetTotal();
            return NoteHelper.GetIndex(actualTotal);
        }

        private uint GetOctave()
        {
            var actualTotal = GetTotal();
            return NoteHelper.GetOctave(actualTotal);
        }
    }
}
