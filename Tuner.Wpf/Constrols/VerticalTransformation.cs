using System.Windows;

namespace Tuner.Wpf.Constrols
{
    class VerticalTransformation : ITransformation
    {
        private double CorrectWidth { get { return Parent.Width < Parent.ActualWidth ? Parent.Width : Parent.ActualWidth; } }

        private double CorrectHeight { get { return Parent.Height < Parent.ActualHeight ? Parent.Height : Parent.ActualHeight; } }
        public double Radius
        {
            get
            {
                double radius = CorrectWidth / 2;

                if (CorrectWidth > CorrectHeight)
                {
                    radius = CorrectHeight / 2;
                }
                return radius;
            }
        }

        public FrameworkElement Parent { get; }

        public double Height
        {
            get { return Parent.ActualHeight; }
        }

        public double Width
        {
            get { return Parent.ActualWidth; }
        }


        public VerticalTransformation(FrameworkElement parent)
        {
            Parent = parent;
        }

        public Point FromUiPoint(Point source)
        {
            return source;
        }

        public Point GetUIPoint(Point source)
        {
            return source;
        }
    }
}