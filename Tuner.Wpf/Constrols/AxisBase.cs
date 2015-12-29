using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tuner.Wpf.Constrols
{
    public class AxisBase  : FrameworkElement
    {
        private VisualCollection theVisuals;
        private DrawingVisual _drawingVisual;

        protected ITransformation Transformation { set; get; }

        public static readonly DependencyProperty MajorStrokeProperty = DependencyProperty.Register(
            "MinorMinorStroke", typeof (Brush), typeof (AxisBase), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush MajorStroke
        {
            get { return (Brush) GetValue(MajorStrokeProperty); }
            set { SetValue(MajorStrokeProperty, value); }
        }

        public static readonly DependencyProperty MajorStrokeThicknessProperty = DependencyProperty.Register(
            "MajorStrokeThickness", typeof (double), typeof (AxisBase), new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double MajorStrokeThickness
        {
            get { return (double) GetValue(MajorStrokeThicknessProperty); }
            set { SetValue(MajorStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty MajorStepProperty = DependencyProperty.Register(
            "MajorStep", typeof (double), typeof (AxisBase), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double MajorStep
        {
            get { return (double) GetValue(MajorStepProperty); }
            set { SetValue(MajorStepProperty, value); }
        }

        public static readonly DependencyProperty MajorHeightProperty = DependencyProperty.Register(
            "MajorHeight", typeof (double), typeof (AxisBase), new FrameworkPropertyMetadata(15.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double MajorHeight
        {
            get { return (double) GetValue(MajorHeightProperty); }
            set { SetValue(MajorHeightProperty, value); }
        }

        public static readonly DependencyProperty MinorStrokeProperty = DependencyProperty.Register(
            "MinorStroke", typeof (Brush), typeof (AxisBase), new FrameworkPropertyMetadata(Brushes.Red,FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush MinorStroke
        {
            get { return (Brush) GetValue(MinorStrokeProperty); }
            set { SetValue(MinorStrokeProperty, value); }
        }

        public static readonly DependencyProperty MinorHeightProperty = DependencyProperty.Register(
            "MinorHeight", typeof (double), typeof (AxisBase), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double MinorHeight
        {
            get { return (double) GetValue(MinorHeightProperty); }
            set { SetValue(MinorHeightProperty, value); }
        }

        public static readonly DependencyProperty MinorStrokeThicknessProperty = DependencyProperty.Register(
            "MinorStrokeThickness", typeof (double), typeof (AxisBase), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double MinorStrokeThickness
        {
            get { return (double) GetValue(MinorStrokeThicknessProperty); }
            set { SetValue(MinorStrokeThicknessProperty, value); }
        }

        static AxisBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AxisBase), new FrameworkPropertyMetadata(typeof(AxisBase)));
        }

        public AxisBase()
        {

            theVisuals = new VisualCollection(this);
            _drawingVisual = new DrawingVisual();
            theVisuals.Add(_drawingVisual);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if(Transformation==null) return;
            int majorCount = (int) (Transformation.Width / MajorStep);
            DrawLines(drawingContext, MajorStep/2, majorCount,MajorStep, MajorStrokeThickness, MajorStroke, MajorHeight);
            DrawLines(drawingContext, MajorStep, majorCount-1, MajorStep, MinorStrokeThickness, MinorStroke, MinorHeight);
            base.OnRender(drawingContext);
        }

        private void DrawLines(DrawingContext drawingContext, double offset, int count, double step, double strokeThickness, Brush stroke, double height)
        {
            var transform = Transformation;
            if (transform == null) return;
            for (int i = 0; i < count; i++)
            {
                drawingContext.DrawLine(new Pen(stroke, strokeThickness), transform.GetUIPoint(new Point(offset+i * step, 0)), transform.GetUIPoint(new Point(offset+i * step, height)));
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

    class CircularAxis : AxisBase
    {
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle", typeof (double), typeof (CircularAxis), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof (double), typeof (CircularAxis), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double Angle
        {
            get { return (double) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty RadiusOffsetProperty = DependencyProperty.Register(
            "RadiusOffset", typeof (double), typeof (CircularAxis), new FrameworkPropertyMetadata(default(double),FrameworkPropertyMetadataOptions.AffectsRender));

        public double RadiusOffset
        {
            get { return (double) GetValue(RadiusOffsetProperty); }
            set { SetValue(RadiusOffsetProperty, value); }
        }

        public CircularAxis()
        {
            
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Transformation = new PolarTransformation(this, StartAngle, Angle, RadiusOffset);
            base.OnRender(drawingContext);
        }
    }
}
