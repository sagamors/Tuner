using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tuner.Wpf.Core;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for AddNewPresetWindow.xaml
    /// </summary>
    public partial class AddNewPresetWindow : WindowDialogBase, IAddNewPresetView
    {
        public AddNewPresetWindow()
        {
            InitializeComponent();
        }
    }
}
