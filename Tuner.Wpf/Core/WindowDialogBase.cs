using System.Windows;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    public class WindowDialogBase : Window, IDialogView
    {
        public void ShowDialog(object owner)
        {
            var ownerWindow = owner as Window;
            Owner = ownerWindow;
            this.ShowDialog();
            this.Close();
        }
    }
}
