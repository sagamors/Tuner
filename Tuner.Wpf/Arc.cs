using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tuner.Wpf
{
    public class Arc : Shape
    {
        private const double MINIMUM_DELTA_ANGLE = 0.1;
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof (double ), typeof (Arc), new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Angle
        {
            get { return (double ) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle", typeof (double), typeof (Arc), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                double radius = ActualWidth;

                if (ActualWidth > ActualHeight)
                {
                    radius = ActualHeight;
                }

                radius /= 2;
                double halfThickness = StrokeThickness/2;
                if (radius >= halfThickness)
                    radius -= halfThickness;
                var center = new Point(ActualWidth/2, ActualHeight/2);
                double endAngle = (StartAngle + Angle);
                double startAngleInRads = ConvertToRads(StartAngle);
                double endAngleInRads = ConvertToRads(endAngle);
                Point p1 = center + new Vector(Math.Sin(endAngle*3.14/180), Math.Cos(endAngleInRads))*radius;
                Point p0 = center + new Vector(Math.Sin(startAngleInRads), Math.Cos(startAngleInRads))*radius;
                PathGeometry geom = new PathGeometry();
                PathFigure fig = new PathFigure();
                fig.IsClosed = (360 - Angle) < MINIMUM_DELTA_ANGLE;
                fig.Segments.Add(new ArcSegment(p0, new Size(radius, radius), 0, Angle > 180, SweepDirection.Clockwise, true));
                fig.StartPoint = p1;
                geom.Figures.Add(fig);
                return geom;
            }
        }

        private double ConvertToRads(double grad)
        {
            return grad * Math.PI/180;
        }
    }
}
