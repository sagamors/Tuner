using System;
using NAudio.CoreAudioApi;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    public interface INoteCapture
    {
        event EventHandler<NoteDetectedEvent> NoteDetected;
        uint SampleRate { set; get; }
        MMDevice Device { set; get; }
        void Start();
        void Stop();
    }

    public class NoteDetectedEvent : EventArgs
    {
        public INote NearestNote { private set; get; }
        public double Frequency { private set; get; }
        public NoteDetectedEvent(INote nearestNote, double frequency)
        {
            NearestNote = nearestNote;
            Frequency = frequency;
        }
    }

}
