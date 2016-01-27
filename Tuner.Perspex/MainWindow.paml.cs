using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace Tuner.Perspex
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            App.AttachDevTools(this);
            this.Find<Button>("Bu").Click += MainWindow_Click;
        }

        private void MainWindow_Click(object sender, global::Perspex.Interactivity.RoutedEventArgs e)
        {
            this.FindControl<Arc>("Arc").Angle++;
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}
