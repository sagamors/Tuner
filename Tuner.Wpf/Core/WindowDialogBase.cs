using System.Windows;
using Tuner.Wpf.ViewModels;

namespace Tuner.Wpf.Core
{
    public class WindowDialogBase : Window, IDialogView
    {
        public bool? ShowDialog(object owner)
        {
            var ownerWindow = owner as Window;
            Owner = ownerWindow;
            
            return this.ShowDialog();
        }
        public void Show(object owner)
        {
            var ownerWindow = owner as Window;
            Owner = ownerWindow;
            this.Show();
        }
    }
}
