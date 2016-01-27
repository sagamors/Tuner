using System;
using System.Collections.Generic;
using Perspex;
using Perspex.Controls;
using Perspex.Controls.Shapes;
using Perspex.Media;
using Perspex.VisualTree;

namespace Tuner.Perspex
{
    public class CircularProgressBar : Control
    {

        private const string _partArcName = "PART_Arc";
        private Arc _partArc;

        public static readonly PerspexProperty<double> StartAngleProperty = PerspexProperty.Register<CircularProgressBar, double>("StartAngle");

        public double StartAngle
        {
            get { return GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }


        public static readonly PerspexProperty<double> ValueProperty = PerspexProperty.Register<CircularProgressBar, double>("Value",notifying:
            (o, b) =>
            {
                var control = o as CircularProgressBar;
                if (control != null) control.SetAngle();
            });

        public double Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly PerspexProperty<Brush> ProgressBrushProperty = PerspexProperty.Register<CircularProgressBar, Brush>("ProgressBrush", Brushes.Black);

        public  Brush ProgressBrush
        {
            get { return GetValue(ProgressBrushProperty); }
            set { SetValue(ProgressBrushProperty, value); }
        }

        public static readonly PerspexProperty<double> ProgressThicknessProperty = PerspexProperty.Register<CircularProgressBar, double>("ProgressThickness",5.0);

        public double ProgressThickness
        {
            get { return GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }


        public static readonly PerspexProperty<double> MinimumProperty = PerspexProperty.Register<CircularProgressBar, double>("Minimum");

        public double Minimum
        {
            get { return GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly PerspexProperty<double> MaximumProperty = PerspexProperty.Register<CircularProgressBar, double>("Maximum", 100.0);

        public double Maximum
        {
            get { return GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly PerspexProperty<Brush> ProgressBackgroundProperty = PerspexProperty.Register<CircularProgressBar, Brush>("ProgressBackground", Brushes.CornflowerBlue);

        public Brush ProgressBackground
        {
            get { return GetValue(ProgressBackgroundProperty); }
            set { SetValue(ProgressBackgroundProperty, value); }
        }

        public static readonly PerspexProperty<SweepDirection> SweepDirectionProperty = PerspexProperty.Register<CircularProgressBar, SweepDirection>("SweepDirection");

        public SweepDirection SweepDirection
        {
            get { return GetValue(SweepDirectionProperty); }
            set { SetValue(SweepDirectionProperty, value); }
        }

        public CircularProgressBar()
        {
;
        }

        public override void ApplyTemplate()
        {
            var arc = new Arc() { Name = _partArcName, Height = 100, Width = 100 };
            arc.Stroke = Brushes.Black;
            //arc.BindTwoWay(Arc.StartAngleProperty, this, StartAngleProperty);
            //arc.Bind(Shape.StrokeThicknessProperty, this, ProgressThicknessProperty);
            //arc.Bind(Shape.StrokeProperty, this, ProgressBrushProperty);
            this.VisualChildren.Add(arc);
            _partArc = arc;
            base.ApplyTemplate();
        }

        private void SetAngle()
        {
           double deltaAngle = 360 / Math.Abs(Maximum - Minimum);

            if (_partArc != null)
            {
                _partArc.StrokeThickness++;

                _partArc.SetValue(Arc.StartAngleProperty, (Value < Maximum) ? deltaAngle * Value : 360);
            }
        }
    }
}
