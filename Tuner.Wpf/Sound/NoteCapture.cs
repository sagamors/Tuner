using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    public class NoteDetectedEvent : EventArgs
    {
        public INote Note {private set; get; }
        public NoteDetectedEvent(INote note)
        {
            Note = note;
        }
    }

    public interface INoteCapture
    {
        event EventHandler<NoteDetectedEvent> NoteDetected;
        uint SampleRate { set; get; }
        MMDevice Device { set; get; }
        void Start();
        void Stop();
    }

    class NoteCapture : INoteCapture
    {
        // There might be a sample aggregator in NAudio somewhere but I made a variation for my needs
        private static int fftLength = 2048; // NAudio fft wants powers of two!
        private SampleAggregator sampleAggregator;
        private const double MinFreq = 60;
        private const double MaxFreq = 1300;
        private WasapiCapture _waveIn;
        public uint SampleRate { set; get; }
        public MMDevice Device { set; get; }
        public INoteFinder NoteFinder {private set; get; }

        public event EventHandler<NoteDetectedEvent> NoteDetected;
        public NoteCapture(INoteFinder noteFinder)
        {
            SampleRate = 44100;
            sampleAggregator = new SampleAggregator(fftLength);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;
            NoteFinder = noteFinder;
        }

        public void Start()
        {
            ResetSampleAggregator();
            _waveIn = new WasapiCapture(Device);
            _waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(OnDataAvailable);
            //_waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(OnRecordingStopped);
            sampleAggregator = new SampleAggregator(fftLength);
            _waveIn.StartRecording();
        }

        public void Stop()
        {
            _waveIn.StopRecording();
            _waveIn.Dispose();
        }

        void FftCalculated(object sender, FftEventArgs e)
        {
            double[] spect = new double[e.Result.Length];
            var l = e.Result.Length;

            for (int i = 0; i < l; i++)
            {
                spect[i] = e.Result[i].X * e.Result[i].X + e.Result[i].Y * e.Result[i].Y;
            }

            double freq = FrequencyUtils.FindFundamentalFrequency(spect, sampleAggregator.Values, SampleRate, MinFreq, MaxFreq);
            var note = NoteFinder.FindNearestNote(freq);
            OnNoteDetected(note);
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            int bufferIncrement = _waveIn.WaveFormat.BlockAlign;

            for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            {
                float sample32 = BitConverter.ToSingle(buffer, index);
                sampleAggregator.Add(sample32);
            }
        }

        public void ResetSampleAggregator()
        {
            sampleAggregator.FftCalculated -= new EventHandler<FftEventArgs>(FftCalculated);

            sampleAggregator = new SampleAggregator(fftLength);
            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;
        }

        private void OnNoteDetected(INote note)
        {
            NoteDetected?.Invoke(this, new NoteDetectedEvent(note));
        }
    }
}
