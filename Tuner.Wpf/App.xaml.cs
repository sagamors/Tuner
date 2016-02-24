using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tuner.Wpf.ViewModels;
using WpfBindingErrors;

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
            base.OnStartup(e);
            BindingExceptionThrower.Attach();
            IMainWindowViewModel window = Bootstrapper.Container.Get<IMainWindowViewModel>();
            var main = window.View;
            MainWindow = (MainWindow)main;

            if (window.Validate())
            { 
                window.AcceptSettings();
                window.Show(null);
            }
            else
            {
                main.Close();
            }
        }
    }
}
