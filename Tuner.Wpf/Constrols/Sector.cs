using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tuner.Wpf.Constrols
{
    public class Sector : Shape
    {
        public double Radius
        {
            get
            {
                double radius = CorrectedWidth / 2;

                if (CorrectedWidth > CorrectedHeight)
                {
                    radius = CorrectedHeight / 2;
                }
                return radius;
            }
        }
        private double CorrectedWidth
        {
            get { return ActualWidth - StrokeThickness / 2; }
        }

        private double CorrectedHeight
        {
            get { return ActualHeight - StrokeThickness / 2; }
        }

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof (double), typeof (Sector),
            new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Angle
        {
            get { return (double) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle", typeof (double), typeof (Sector),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty SweepDirectionProperty = DependencyProperty.Register(
                "SweepDirection", typeof(SweepDirection), typeof(Sector), new FrameworkPropertyMetadata(default(SweepDirection), FrameworkPropertyMetadataOptions.AffectsRender));

        public SweepDirection SweepDirection
        {
            get { return (SweepDirection) GetValue(SweepDirectionProperty); }
            set { SetValue(SweepDirectionProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", typeof (double), typeof (Sector), new FrameworkPropertyMetadata(default(double),FrameworkPropertyMetadataOptions.AffectsRender));

        public double Thickness
        {
            get { return (double) GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {   // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry
                {
                    FillRule = FillRule.EvenOdd
                };

                if (CorrectedWidth < 0 || CorrectedHeight < 0)
                {
                    return geometry;
                }

                var center = new Point(CorrectedWidth / 2, CorrectedHeight / 2);
                double startAngleInRads = MathHelper.ConvertToRads(StartAngle);
                double endAngle = (StartAngle + Angle);
                double endAngleInRads = MathHelper.ConvertToRads(endAngle);
                int direction = (SweepDirection == SweepDirection.Clockwise ? 1 : -1);
                var beginVector = new Vector(Math.Cos(startAngleInRads), Math.Sin(startAngleInRads) * direction);
                //lower left point
                Point beginLeftPoint = center + beginVector * (Radius - Thickness);
                //bottom right point
                Point beginningRightPoint = center + beginVector * Radius;
                var endVector = new Vector(Math.Cos(endAngleInRads), Math.Sin(endAngleInRads) * direction);
                Point endRightPoint = center + endVector * Radius;
                var smallRadius = Radius - Thickness;
                Point endLeftPoint = center + endVector * smallRadius;
                using (StreamGeometryContext context = geometry.Open())
                {
                    context.BeginFigure(beginLeftPoint,true,true);
                    context.LineTo(beginningRightPoint,true,true );
                    context.ArcTo(endRightPoint,new Size(Radius , Radius),0,false, SweepDirection, true,true);
                    context.LineTo(endLeftPoint, true, true);
                    context.ArcTo(beginLeftPoint, new Size(smallRadius, smallRadius), 0, false, (SweepDirection == SweepDirection.Clockwise ? SweepDirection.Counterclockwise : SweepDirection.Clockwise), true, true);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
    }
}
