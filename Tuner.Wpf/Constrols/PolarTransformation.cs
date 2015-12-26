using System;
using System.Windows;
using System.Windows.Media;

namespace Tuner.Wpf.Constrols
{
    class PolarTransformation : ITransformation
    {
        private double _radiusOffset;
        public double StartAngle { get; }
        public double Angle {  get; }
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

        public double Height { get { return Radius; } }

        public double Width
        {
            get { return Angle; }

        }

        public PolarTransformation(FrameworkElement parent, double startAngle, double angle, double radiusOffset)
        {
            Parent = parent;
            StartAngle = startAngle;
            Angle = angle;
            _radiusOffset = radiusOffset;
        }

        public Point FromUiPoint(Point source)
        {
            return source;
        }

        public Point GetUIPoint(Point source)
        {
            double radius = Radius;
            radius -= (source.Y + _radiusOffset);
            var center = new Point(CorrectWidth / 2, CorrectHeight / 2);
            double endAngle = MathHelper.ConvertToRads((StartAngle + source.X));
            Point startPoint = center + new Vector(Math.Cos(endAngle), -Math.Sin(endAngle)) * radius;
            return startPoint;
        }
    }
}