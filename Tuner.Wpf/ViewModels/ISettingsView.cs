using Tuner.Wpf.Core;

namespace Tuner.Wpf.ViewModels
{
    public interface ISettingsView : IView
    {
        void ShowDialog(object owner);
        void Close();
    }
}