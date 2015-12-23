using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tuner.Wpf.Constrols
{
    public class Arc : Shape
    {
        private const double MINIMUM_DELTA_ANGLE = 0.1;

        public double Radius
        {
            get
            {
                double radius = ActualWidth / 2;

                if (ActualWidth > ActualHeight)
                {
                    radius = ActualHeight / 2;
                }
                return radius;
            }
        }


        public static readonly DependencyProperty SweepDirectionProperty = DependencyProperty.Register(
            "SweepDirection", typeof (SweepDirection), typeof (Arc), new FrameworkPropertyMetadata(default(SweepDirection), FrameworkPropertyMetadataOptions.AffectsRender));

        public SweepDirection SweepDirection
        {
            get { return (SweepDirection) GetValue(SweepDirectionProperty); }
            set { SetValue(SweepDirectionProperty, value); }
        }

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

        static Arc()
        {
            var dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(StrokeThicknessProperty, typeof(Arc));
            var metadata = dependencyPropertyDescriptor.Metadata;
            StrokeThicknessProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(metadata.DefaultValue, metadata.PropertyChangedCallback, (o, value) =>
            {
                var control = (Arc)o;
                double radius = control.Radius;

                if (Math.Abs(radius) < 0.1) return value;
                double progressThickness = (double)value;

                if (radius - progressThickness < 0)
                {
                    return radius;
                }

                return value;
            }));
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                double radius = Radius;
                double halfThickness = StrokeThickness/2;
                if (radius >= halfThickness)
                    radius -= halfThickness;

                if (radius < halfThickness)
                {
                    radius = halfThickness;
                }

                var center = new Point(ActualWidth/2, ActualHeight/2);
                double endAngle = (StartAngle + Angle);
                bool isClosed = Math.Abs(StartAngle - endAngle) >=(360 - MINIMUM_DELTA_ANGLE);

                if (isClosed)
                {
                    endAngle -= 0.1;
                }

                double startAngleInRads = ConvertToRads(StartAngle);
                double endAngleInRads = ConvertToRads(endAngle);
                Point startPoint = center + new Vector(Math.Cos(startAngleInRads), Math.Sin(startAngleInRads)) * radius;
                Point endPoint = center + new Vector(Math.Cos(endAngleInRads), Math.Sin(endAngleInRads) * (SweepDirection == SweepDirection.Clockwise ? 1 : -1)) * radius;
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                figure.IsClosed = isClosed;
                var arcSegment = new ArcSegment(endPoint, new Size(radius, radius), 0, Angle > 180, SweepDirection, true);
                figure.Segments.Add(arcSegment);
                figure.StartPoint = startPoint;
                geometry.Figures.Add(figure);
                return geometry;
            }
        }

        private double ConvertToRads(double grad)
        {
            return grad * Math.PI/180;
        }
    }
}
