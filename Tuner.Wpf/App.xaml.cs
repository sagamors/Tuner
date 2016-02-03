using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Bootstrapper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IMainWindowViewModel window = Bootstrapper.Container.Get<IMainWindowViewModel>();
            MainWindow = (MainWindow)window.View;
            MainWindow.Show();
        }
    }
}
