using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using Tuner.Core;

namespace Tuner.Wpf.Sound
{
    class NoteCapture : INoteCapture
    {
        // There might be a sample aggregator in NAudio somewhere but I made a variation for my needs
        private static int fftLength = 2048; // NAudio FFT wants powers of two!
        private SampleAggregator _sampleAggregator;
        private const double MinFreq = 60;
        private const double MaxFreq = 1300;
        private WasapiCapture _waveIn;
        private bool _isStarting;
        public uint SampleRate { set; get; }
        public MMDevice Device { set; get; }
        public INoteFinder NoteFinder { get; }
        public event EventHandler<NoteDetectedEvent> NoteDetected;
        public NoteCapture(INoteFinder noteFinder)
        {
            SampleRate = 44100;
            _sampleAggregator = new SampleAggregator(fftLength);
            _sampleAggregator.FftCalculated += FftCalculated;
            _sampleAggregator.PerformFFT = true;
            NoteFinder = noteFinder;
        }

        public void Start()
        {
            if (_isStarting) return;
            _isStarting = true;
            ResetSampleAggregator();
            _waveIn = new WasapiCapture(Device);
            _waveIn.DataAvailable +=OnDataAvailable;
            //_waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(OnRecordingStopped);
            _sampleAggregator = new SampleAggregator(fftLength);
            _waveIn.StartRecording();
        }

        public void Stop()
        {
            _isStarting = false;
            if (_waveIn==null) return;
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

            double freq = FrequencyUtils.FindFundamentalFrequency(spect, _sampleAggregator.Values, SampleRate, MinFreq, MaxFreq);
            var note = NoteFinder.FindNearestNote(freq);
            OnNoteDetected(note, freq);
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            int bufferIncrement = _waveIn.WaveFormat.BlockAlign;

            for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            {
                float sample32 = BitConverter.ToSingle(buffer, index);
                _sampleAggregator.Add(sample32);
            }
        }

        public void ResetSampleAggregator()
        {
            _sampleAggregator.FftCalculated -= FftCalculated;
            _sampleAggregator = new SampleAggregator(fftLength);
            _sampleAggregator.FftCalculated += FftCalculated;
            _sampleAggregator.PerformFFT = true;
        }

        private void OnNoteDetected(INote note, double frequency)
        {
            NoteDetected?.Invoke(this, new NoteDetectedEvent(note, frequency));
        }
    }
}
