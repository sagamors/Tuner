using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PropertyChanged;
using Tuner.Wpf.Constrols;
using System.Windows.Controls;

namespace Tuner.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ImplementPropertyChanged]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public double Radius { set; get; }
        public double Angle { set; get; }
        public double StartAngle { set; get; }
        public String TunedString { set; get; }

        public MainWindow()
        {
            Radius = 50;
            InitializeComponent();
            SizeChanged += MainWindow_SizeChanged;
            this.LocationChanged += MainWindow_LocationChanged;
            this.StateChanged += MainWindow_StateChanged;
        }

        private void MainWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
               // TunePopup.IsOpen = false;
            }
            TunePopupToString();
        }

        private void MainWindow_LocationChanged(object sender, System.EventArgs e)
        {
            TunePopupToString();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TunePopupToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TunedString = sender as String;
            TunePopupToString();
        }

        private void TunePopupToString()
        {
            if(TunedString==null) return;
            Point relativePoint = TunedString.TransformToAncestor(this).Transform(new Point(0, 0));
            /*
            TunePopup.VerticalOffset++;
            TunePopup.HorizontalOffset++;
            TunePopup.VerticalOffset = (relativePoint.Y + TunedString.ActualHeight / 2)- TunePopup.Child.RenderSize.Height / 2;
            TunePopup.HorizontalOffset = 10;
            */
        }
    }
}
