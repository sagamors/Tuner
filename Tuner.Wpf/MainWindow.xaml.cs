using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
                OnPropertyChanged();
            }
            get { return _angle; }
        }

        public double StartAngle
        {
            set
            {
                _startAngle = value;
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
