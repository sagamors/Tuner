using NAudio.Dsp;
using System;

namespace Tuner.Wpf.Sound
{
    class SampleAggregator
    {
        private readonly FftEventArgs _fftArgs;
        private int _fftPos;
        private readonly int _fftLength;
        private int m;
        private readonly Complex[] _fftBuffer;

        public event EventHandler<FftEventArgs> FftCalculated;
        public bool PerformFFT { get; set; }
        public float[] Values { set; get; }

        public SampleAggregator(int fftLength)
        {
            if (!IsPowerOfTwo(fftLength))
            {
                throw new ArgumentException("FFT Length must be a power of two");
            }
            this.m = (int)Math.Log(fftLength, 2.0);
            this._fftLength = fftLength;
            this._fftBuffer = new Complex[fftLength];
            Values = new float[fftLength];
            this._fftArgs = new FftEventArgs(_fftBuffer);
        }

        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public void Add(float value)
        {
            if (PerformFFT && FftCalculated != null)
            {
                // Remember the window function! There are many others as well.
                Values[_fftPos] = (float)(value * FastFourierTransform.HammingWindow(_fftPos, _fftLength));
                _fftBuffer[_fftPos].X = Values[_fftPos];
                _fftBuffer[_fftPos].Y = 0; // This is always zero with audio.
                _fftPos++;
                if (_fftPos >= _fftLength)
                {
                    _fftPos = 0;
                    FastFourierTransform.FFT(true, m, _fftBuffer);
                    FftCalculated(this, _fftArgs);
                }
            }
        }
    }
}
