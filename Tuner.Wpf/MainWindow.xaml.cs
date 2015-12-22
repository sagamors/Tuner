using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tuner.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public double Radius { set; get; }
        private double _angle;
        private double _startAngle;

        public double Angle
        {
            set
            {
                _angle = value;
                //RecalcParams();
                OnPropertyChanged();

            }
            get { return _angle; }
        }

        public double StartAngle
        {
            set
            {
                _startAngle = value;
                //RecalcParams();
                OnPropertyChanged();
            }
            get { return _startAngle; }
        }


        public MainWindow()
        {
            Radius = 50;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
