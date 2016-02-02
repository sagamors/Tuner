using System;
using System.Windows;
using Tuner.Wpf.Core;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, ISettingsView
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void ShowDialog(object owner)
        {
            this.ShowDialog();
        }
    }
}
