using Perspex.Controls;
using Perspex.Markup.Xaml;

namespace Tuner.Perspex
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.AttachDevTools(this);
        }

        private void InitializeComponent()
        {
            PerspexXamlLoader.Load(this);
        }
    }
}
