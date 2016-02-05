using System;
using System.Windows;
using Tuner.Wpf.Core;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : WindowDialogBase, ISettingsView
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
