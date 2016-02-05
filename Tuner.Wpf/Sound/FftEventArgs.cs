using NAudio.Dsp;
using System;
using System.Diagnostics;

namespace Tuner.Wpf.Sound
{
    public class FftEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public FftEventArgs(Complex[] result)
        {
            this.Result = result;
        }
        public Complex[] Result { get; private set; }
    }
}
