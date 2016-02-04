using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tuner.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NoteFinderTest()
        {
            NoteFactory noteFactory = new NoteFactory();
            NoteFinder noteFinder = new NoteFinder(noteFactory);
            Note note = new Note("C",2,110);
            var actual = noteFinder.FindNearestNote(110);
            var v = actual == note;
            Assert.AreEqual(actual, note);
        }
    }
}
