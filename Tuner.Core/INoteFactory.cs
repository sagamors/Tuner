using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Core
{
    public interface INoteFactory
    {
        double MainFrequnect { get; }
        INote CreateNote(uint indexNote, uint octave, double frequency);
    }
}
