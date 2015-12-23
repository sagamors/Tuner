using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Perspex
{
    public class MainWindowViewModel : ReactiveObject
    {
        private double _doubleValue = 5.0;
        public double DoubleValue
        {
            get { return _doubleValue; }
            set { this.RaiseAndSetIfChanged(ref _doubleValue, value); }
        }
    }
}
