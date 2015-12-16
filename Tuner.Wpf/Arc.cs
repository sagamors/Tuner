using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tuner.Wpf
{

    //todo how to set a limit 
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

        static Arc()
        {
            var dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(StrokeThicknessProperty, typeof(Arc));
            var metadata = dependencyPropertyDescriptor.Metadata;
            StrokeThicknessProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(metadata.DefaultValue, metadata.PropertyChangedCallback, (o, value) =>
            {
                var control = (Arc)o;
                double radius = control.ActualWidth / 2;

                if (control.ActualWidth > control.ActualHeight)
                {
                    radius = control.ActualHeight / 2;
                }

                if (Math.Abs(radius) < 0.1) return value;
                double progressThickness = (double)value;

                if (radius - progressThickness < 0)
                {
                    return radius;
                }

                return value;
            }));
        }

        public Arc()
        {


            //DependencyPropertyDescriptor.FromProperty(StrokeThicknessProperty, typeof (Arc))
            //    .Metadata.CoerceValueCallback += (o, value) =>
            //    {
            //        var control = (CircularProgressBar)o;
            //        double radius = control.ActualWidth / 2;

            //        if (control.ActualWidth > control.ActualHeight)
            //        {
            //            radius = control.ActualHeight / 2;
            //        }

            //        if (Math.Abs(radius) < 0.1) return value;
            //        double progressThickness = (double)value;

            //        if (radius - progressThickness < 0)
            //        {
            //            return radius;
            //        }

            //        return value;
            //    };
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
                Point p1 = center + new Vector(Math.Sin(endAngleInRads), Math.Cos(endAngleInRads))*radius;
                Point p0 = center + new Vector(Math.Sin(startAngleInRads), Math.Cos(startAngleInRads))*radius;
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                figure.IsClosed = isClosed;
                var arcSegment = new ArcSegment(p0, new Size(radius, radius), 0, Angle > 180, SweepDirection.Clockwise, true);
                figure.Segments.Add(arcSegment);
                figure.StartPoint = p1;
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
