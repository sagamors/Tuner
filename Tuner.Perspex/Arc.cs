using System;
using Perspex;
using Perspex.Controls.Shapes;
using Perspex.Media;
using Geometry = Perspex.Media.Geometry;
using SweepDirection = Perspex.Media.SweepDirection;

namespace Tuner.Perspex
{
    public class Arc : Shape
    {
        private const double MINIMUM_DELTA_ANGLE = 0.1;

        public double Radius
        {
            get
            {
                double radius = this.Width / 2;

                if (Width > Height)
                {
                    radius = Height / 2;
                }
                return radius;
            }
        }

        public static readonly PerspexProperty<SweepDirection> SweerDirectionProperty = PerspexProperty.Register<Arc, SweepDirection>("SweepDirection");

        public SweepDirection SweepDirection
        {
            get { return GetValue(SweerDirectionProperty); }
            set { SetValue(SweerDirectionProperty, value); }
        }


        public static readonly PerspexProperty<double> AngleProperty = PerspexProperty.Register<Arc, double>("Angle");

        public double Angle
        {
            get { return GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }


        public static readonly PerspexProperty<double> StartAngleProperty = PerspexProperty.Register<Arc, double>("StartAngle");

        public double StartAngle
        {
            get { return GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }


        static Arc()
        {

            AffectsRender(AngleProperty);
            AffectsRender(SweerDirectionProperty);
            AffectsRender(StartAngleProperty);

            StrokeThicknessProperty.OverrideValidation<Arc>((o, value) =>
            {
                var control = o;
                double radius = control.Radius;

                if (Math.Abs(radius) < 0.1) return value;
                double progressThickness = (double)value;

                if (radius - progressThickness < 0)
                {
                    return radius;
                }

                return value;
            });
        }



       public override Geometry DefiningGeometry
        {
            get
            {
                double radius = Radius;
                double halfThickness = StrokeThickness / 2;
                if (radius >= halfThickness)
                    radius -= halfThickness;

                if (radius < halfThickness)
                {
                    radius = halfThickness;
                }
                
                var center = new Point(Width / 2, Height / 2);
                double endAngle = (StartAngle + Angle);
                bool isClosed = Math.Abs(StartAngle - endAngle) >= (360 - MINIMUM_DELTA_ANGLE);

                if (isClosed)
                {
                    endAngle -= 0.1;
                }

                double startAngleInRads = ConvertToRads(StartAngle);
                double endAngleInRads = ConvertToRads(endAngle);
                Point endPoint;
                Point startPoint;
                var vec = new Vector(Math.Cos(endAngleInRads), Math.Sin(endAngleInRads));

                if (SweepDirection == SweepDirection.Clockwise)
                {
                    endPoint = new Point(center.X + vec.X * radius, center.Y + vec.Y * radius); 
                }
                else
                {
                    endPoint = new Point(center.X + vec.X * radius, -center.Y + vec.Y * radius);
                }
                startPoint = new Point(center.X+ Math.Cos(startAngleInRads), Math.Sin(startAngleInRads)*radius); 
                var arcSegment = new ArcSegmentGeometry(startPoint,endPoint, new Size(radius, radius), 0, Angle > 180, SweepDirection,isClosed,true);
                return arcSegment;
            }
        }

        private double ConvertToRads(double grad)
        {
            return grad * Math.PI / 180;
        }
    }
}
