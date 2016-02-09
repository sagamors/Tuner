using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tuner.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        //C#1
        private double frequency = 34.65;
        private uint totalIndex = 13;
        private uint octave = 1;
        private uint index = 1;

        [TestMethod]
        public void NoteFinderTest()
        {
            NoteFactory noteFactory = new NoteFactory();
            NoteFinder noteFinder = new NoteFinder(noteFactory);
            INote note = noteFactory.CreateNote(eNote.C, 2);
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
            Assert.AreEqual(index, actualOctave);
        }

        private uint GetTotal()
        {
             return FrequencyUtils.GetTotalIndexNote(frequency, 440);
        }

        private uint GetIndex()
        {
            var actualTotal = GetTotal();
            return FrequencyUtils.GetIndex(actualTotal);
        }

        private uint GetOctave()
        {
            var actualTotal = GetTotal();
            return FrequencyUtils.GetOctave(actualTotal);
        }
    }
}
