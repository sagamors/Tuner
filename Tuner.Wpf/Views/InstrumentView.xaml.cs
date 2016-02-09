using System.Windows;
using System.Windows.Controls;
using Tuner.Core;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Views
{
    /// <summary>
    /// Interaction logic for InstrumentView.xaml
    /// </summary>
    public partial class InstrumentView : UserControl
    {
        public InstrumentView()
        {
            InitializeComponent();
        }

        private void Note_Checked(object sender, RoutedEventArgs e)
        {
            var control = (sender as RadioButton);
            if(control == null) return;
            var main = Application.Current.MainWindow.DataContext as IMainWindowViewModel;
            //var main = Bootstrapper.Container.Get<IMainWindowViewModel>();
            if (main == null) return;
            var note =  control.Content as INote;
            main.TargetNote = note;
        }
    }
}
