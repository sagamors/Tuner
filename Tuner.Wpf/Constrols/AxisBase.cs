using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Tuner.Wpf.Constrols
{
    public class AxisBase  : FrameworkElement
    {
        private const string TYPEFACE_NAME = "Arial";

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

        public static readonly DependencyProperty TextOffsetProperty = DependencyProperty.Register(
            "TextOffset", typeof(double), typeof(AxisBase), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double TextOffset
        {
            get { return (double)GetValue(TextOffsetProperty); }
            set { SetValue(TextOffsetProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
       "FontSize", typeof(double), typeof(AxisBase), new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
"Foreground", typeof(Brush), typeof(AxisBase), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
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
            //if (true)
            //{
            //    DrawLinesFromCenter(drawingContext, 0, majorCount/2, MajorStep, MajorStrokeThickness, MajorStroke, MajorHeight, true);
            //    DrawLinesFromCenter(drawingContext, MajorStep/2, majorCount - 1, MajorStep, MinorStrokeThickness, MinorStroke, MinorHeight);
            //}
            //else
            {
                DrawLines(drawingContext, MajorStep / 2, majorCount, MajorStep, MajorStrokeThickness, MajorStroke, MajorHeight, true);
                DrawLines(drawingContext, MajorStep, majorCount - 1, MajorStep, MinorStrokeThickness, MinorStroke, MinorHeight);
            }

            base.OnRender(drawingContext);
        }

        private void DrawLines(DrawingContext drawingContext, double offset, int count, double step, double strokeThickness, Brush stroke, double height, bool showText = false)
        {
            var transform = Transformation;
            if (transform == null) return;
   
            for (int i = 0; i < count; i++)
            {
                var value = i * step;
                // todo dummy realization
                var text = value.ToString("f0");
                var x = offset + value;
                var yUI = transform.GetUIPoint(new Point(x, height));
                drawingContext.DrawLine(new Pen(stroke, strokeThickness), transform.GetUIPoint(new Point(x, 0)), yUI);
                if (showText)
                {
                    var formatted = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(TYPEFACE_NAME), FontSize, Foreground);
                    var offsetTextPoint = transform.GetUIPoint(new Point(x, height + TextOffset));
                    drawingContext.DrawText(formatted, new Point(offsetTextPoint.X - formatted.Width/2,offsetTextPoint.Y));
                }
            }
        }

        // todo dummy realization
        private void DrawLinesFromCenter(DrawingContext drawingContext, double offset, int count, double step, double strokeThickness, Brush stroke, double height, bool showText = false)
        {
            var transform = Transformation;
            if (transform == null) return;
            for (int i = 1 - count; i < count; i++)
            {
                var value = i * step;
                // todo dummy realization
                var text = value.ToString("f0");
                var x = offset + value + transform.Width/2;
                var yUI = transform.GetUIPoint(new Point(x, height));
                drawingContext.DrawLine(new Pen(stroke, strokeThickness), transform.GetUIPoint(new Point(x, 0)), yUI);
                if (showText)
                {
                    var formatted = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(TYPEFACE_NAME), FontSize, Foreground);
                    var offsetTextPoint = transform.GetUIPoint(new Point(x, height + TextOffset));
                    drawingContext.DrawText(formatted, new Point(offsetTextPoint.X - formatted.Width / 2, offsetTextPoint.Y));
                }
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

            Sector sector = new  Sector()
            {
                Angle = this.Angle,
                StartAngle = this.StartAngle,
                Fill = Brushes.Beige,
                Thickness = 100
            };
            
            drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.Bisque,2) ,sector.RenderedGeometry);
            //drawingContext.PushClip(sector.RenderedGeometry);
            //Angle = "{TemplateBinding Angle}"
            //                 Fill = "{StaticResource TuneProgress.Gauge.Background}"
            //                 StartAngle = "{TemplateBinding StartAngle}"
            //                 Thickness = "{TemplateBinding Thickness}" />
        }
    }
}
