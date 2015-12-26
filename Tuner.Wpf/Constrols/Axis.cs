using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tuner.Wpf.Constrols
{
    public class Axis  : FrameworkElement
    {
        private VisualCollection theVisuals;
        private DrawingVisual _drawingVisual;

        public static readonly DependencyProperty MajorStrokeProperty = DependencyProperty.Register(
            "MinorMinorStroke", typeof (Brush), typeof (Axis), new PropertyMetadata(Brushes.Black));

        public Brush MajorStroke
        {
            get { return (Brush) GetValue(MajorStrokeProperty); }
            set { SetValue(MajorStrokeProperty, value); }
        }

        public static readonly DependencyProperty MajorStrokeThicknessProperty = DependencyProperty.Register(
            "MajorStrokeThickness", typeof (double), typeof (Axis), new PropertyMetadata(2.0));

        public double MajorStrokeThickness
        {
            get { return (double) GetValue(MajorStrokeThicknessProperty); }
            set { SetValue(MajorStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty MajorStepProperty = DependencyProperty.Register(
            "MajorStep", typeof (double), typeof (Axis), new PropertyMetadata(10.0));

        public double MajorStep
        {
            get { return (double) GetValue(MajorStepProperty); }
            set { SetValue(MajorStepProperty, value); }
        }

        public static readonly DependencyProperty MajorHeightProperty = DependencyProperty.Register(
            "MajorHeight", typeof (double), typeof (Axis), new PropertyMetadata(10.0));

        public double MajorHeight
        {
            get { return (double) GetValue(MajorHeightProperty); }
            set { SetValue(MajorHeightProperty, value); }
        }

        public static readonly DependencyProperty MinorStrokeProperty = DependencyProperty.Register(
            "MinorStroke", typeof (Brush), typeof (Axis), new PropertyMetadata(Brushes.Black));

        public Brush MinorStroke
        {
            get { return (Brush) GetValue(MinorStrokeProperty); }
            set { SetValue(MinorStrokeProperty, value); }
        }

        public static readonly DependencyProperty MinorHeightProperty = DependencyProperty.Register(
            "MinorHeight", typeof (double), typeof (Axis), new PropertyMetadata(5.0));

        public double MinorHeight
        {
            get { return (double) GetValue(MinorHeightProperty); }
            set { SetValue(MinorHeightProperty, value); }
        }

        public static readonly DependencyProperty MinorStrokeThicknessProperty = DependencyProperty.Register(
            "MinorStrokeThickness", typeof (double), typeof (Axis), new PropertyMetadata(1.0));

        public double MinorStrokeThickness
        {
            get { return (double) GetValue(MinorStrokeThicknessProperty); }
            set { SetValue(MinorStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty MinorStepProperty = DependencyProperty.Register(
            "MinorStep", typeof (double ), typeof (Axis), new PropertyMetadata(default(double)));

        public double  MinorStep
        {
            get { return (double ) GetValue(MinorStepProperty); }
            set { SetValue(MinorStepProperty, value); }
        }


        static Axis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Axis), new FrameworkPropertyMetadata(typeof(Axis)));
        }

        public Axis()
        {
            // todo : delete number
            Transformation = new PolarTransformation(this,-45,90,10);
            theVisuals = new VisualCollection(this);
            _drawingVisual = new DrawingVisual();
            theVisuals.Add(_drawingVisual);
        }

        private ITransformation Transformation { set; get; }


        protected override void OnRender(DrawingContext drawingContext)
        {
            if(Transformation==null) return;
            int majorCount = (int) (Transformation.Width / MajorStep);
            DrawLines(drawingContext, majorCount, MajorStep, MajorStrokeThickness, MajorStroke, MajorHeight);
            // todo : add minor draw
            //DrawLines(drawingContext, majorCount, MajorStep, MajorStrokeThickness, MajorStroke);
            int minorCount = (int)(Transformation.Height / MinorStep);
            //DrawLines(drawingContext, minorCount, MinorStep, MinorStrokeThickness, MinorStroke, MinorHeight);
        }

        private void DrawLines(DrawingContext drawingContext, int count, double step, double strokeThickness, Brush stroke, double height)
        {
            var transform = Transformation;
            if (transform == null) return;
            for (int i = 0; i < count; i++)
            {
                drawingContext.DrawLine(new Pen(stroke, strokeThickness), transform.GetUIPoint(new Point(i * step, 0)), transform.GetUIPoint(new Point(i * step, height)));
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return theVisuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return theVisuals.Count; }
        }
    }
}
