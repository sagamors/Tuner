using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Core
{
    public interface INote
    {
        double Frequency { get; }
        string Name { get; }
        uint Octave { get; }
        string FullName { get; }
    }
}
