using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Wpf.Core
{
    public interface IView
    {
        object DataContext { get; set; }
    }

}
