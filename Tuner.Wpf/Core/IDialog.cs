namespace Tuner.Wpf.Core
{
    public interface IDialog
    {
        bool? ShowDialog(object owner);
        void Close();
    }
}
