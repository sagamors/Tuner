using System.Windows;

namespace Tuner.Wpf.Constrols
{
    public interface ITransformation
    {
        double Height { get; }
        double Width { get; }

        Point FromUiPoint(Point source);

        Point GetUIPoint(Point source);
    }
}