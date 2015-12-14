using System.Collections.Generic;
using Perspex.Controls;
using Perspex.Controls.Shapes;
using Perspex.Media;

namespace Tuner.Perspex
{
    public class CircleProgressBar : Control
    {
        public CircleProgressBar()
        {

            var e = new Ellipse();
            var radialGradient = new RadialGradientBrush();
            radialGradient.Radius = 10;
            radialGradient.GradientOrigin = TransformOrigin;
            radialGradient.Center = TransformOrigin;
            radialGradient.GradientStops = new List<GradientStop>();
            radialGradient.GradientStops.Add(new GradientStop(Colors.Aqua, 0.5));
            radialGradient.GradientStops.Add(new GradientStop(Colors.Red, 10.0));
            e.Fill = radialGradient;
            this.AddVisualChild(e);
        }
    }
}
