using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tuner.Wpf.Helpers;

namespace Tuner.Wpf.ViewModels
{
    class MainViewModel
    {
        public ICommand OpenSettingsCommand {private set; get; }

        public MainViewModel()
        {
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        public void OpenSettings()
        {

        }
    }
}
