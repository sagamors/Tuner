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
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof (double), typeof (Sector), new FrameworkPropertyMetadata(default(double),FrameworkPropertyMetadataOptions.AffectsRender));

        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        private double CorrectedWidth
        {
            get { return ActualWidth - StrokeThickness/2; }
        }

        private double CorrectedHeight
        {
            get { return ActualHeight - StrokeThickness/2; }
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

                using (StreamGeometryContext context = geometry.Open())
                {
                    context.BeginFigure(new Point(0, this.CorrectedHeight / 2),true,true);
                    context.LineTo(new Point(CorrectedWidth - Radius,0),true,true );
                    context.ArcTo(new Point(CorrectedWidth - Radius, CorrectedHeight),new Size(Radius, CorrectedHeight),0,false,SweepDirection.Clockwise, true,true);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }
    }
}
