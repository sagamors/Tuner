using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuner.Wpf.ViewModels
{
    public interface IWindowView
    {
        void ShowDialog(object owner);

        void Close();
    }
}
