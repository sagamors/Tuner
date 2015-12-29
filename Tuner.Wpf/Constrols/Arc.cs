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

        private static readonly DependencyPropertyKey RadiusPropertyKey  = DependencyProperty.RegisterReadOnly(
            "Radius", typeof (double), typeof (Arc), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender,
                (o, args) =>
                {
                    var arc = (Arc)o;
                    arc.Diameter = arc.Radius * 2;
                }));

        public static readonly DependencyProperty RadiusProperty = RadiusPropertyKey.DependencyProperty;

        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            protected set { SetValue(RadiusPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey DiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            "Diameter", typeof(double), typeof(Arc), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty DiameterProperty = RadiusPropertyKey.DependencyProperty;

        public double Diameter
        {
            get { return (double) GetValue(DiameterProperty); }
            protected set { SetValue(DiameterPropertyKey, value); }
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

        public Arc()
        {
            this.SizeChanged+= OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            double radius = CorrectedWidth / 2;

            if (CorrectedWidth > CorrectedHeight)
            {
                radius = CorrectedHeight / 2;
            }
            Radius = radius;
        }

        private double CorrectedWidth { get { return Width < ActualWidth ? Width : ActualWidth; } }

        private double CorrectedHeight { get { return Height < ActualHeight ? Height : ActualHeight; } }
        protected override Geometry DefiningGeometry
        {
            get
            {
                double radius = Radius;
                double halfThickness = StrokeThickness / 2;
                if (radius >= halfThickness)
                    radius -= halfThickness;

                if (radius < halfThickness && Math.Abs(radius) > 0.1)
                {
                    radius = Radius / 2;
                    StrokeThickness = radius * 2;
                }

                var center = new Point(CorrectedWidth / 2, CorrectedHeight / 2);
                double endAngle = (StartAngle + Angle);
                bool isClosed = Math.Abs(StartAngle - endAngle) >= (360 - MINIMUM_DELTA_ANGLE);
                if (isClosed)
                {
                    return new EllipseGeometry(center, radius, radius);
                }
                double startAngleInRads = MathHelper.ConvertToRads(StartAngle);
                double endAngleInRads = MathHelper.ConvertToRads(endAngle);
                int direction = (SweepDirection == SweepDirection.Clockwise ? 1 : -1);
                Point startPoint = center + new Vector(Math.Cos(startAngleInRads), Math.Sin(startAngleInRads) * direction) * radius;
                Point endPoint = center + new Vector(Math.Cos(endAngleInRads), Math.Sin(endAngleInRads) * direction) * radius;
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                figure.IsFilled = true;
                var arcSegment = new ArcSegment(endPoint, new Size(radius, radius), 0, Angle > 180, SweepDirection, true);
                figure.Segments.Add(arcSegment);
                figure.StartPoint = startPoint;
                geometry.Figures.Add(figure);

                // Create a StreamGeometry for describing the shape
                //StreamGeometry geometry = new StreamGeometry
                //{
                //    FillRule = FillRule.EvenOdd
                //};

                //if (CorrectedWidth < 0 || CorrectedHeight < 0)
                //{
                //    return geometry;
                //}

                //using (StreamGeometryContext context = geometry.Open())
                //{
                //    context.BeginFigure(new Point(0, this.CorrectedHeight / 2), true, true);
                //    context.LineTo(new Point(CorrectedWidth - Radius, 0), true, true);
                //    context.ArcTo(new Point(CorrectedWidth - Radius, CorrectedHeight), new Size(Radius, CorrectedHeight),
                //        0, false, SweepDirection.Clockwise, true, true);
                //}
                //// Freeze the geometry for performance benefits
                //geometry.Freeze();

                return geometry;
            }
        }

        public double OuterCircularBorder
        {
            get { return (double)GetValue(CircularProgressBar.OuterCircularBorderThicknessProperty); }
            set { SetValue(CircularProgressBar.OuterCircularBorderThicknessProperty, value); }
        }
    }
}
