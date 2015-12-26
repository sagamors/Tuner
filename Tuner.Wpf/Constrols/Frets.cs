using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tuner.Wpf.Constrols
{
    public class Frets : FrameworkElement
    {
        private VisualCollection theVisuals;
        private DrawingVisual _drawingVisual;

        static Frets()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (Frets), new FrameworkPropertyMetadata(typeof (Frets)));
        }

        public Frets()
        {
            theVisuals = new VisualCollection(this);
            _drawingVisual = new DrawingVisual();
            theVisuals.Add(_drawingVisual);
        }

        protected override Visual GetVisualChild(int index)
        {
            return theVisuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return theVisuals.Count; }
        }

        public static readonly DependencyProperty NumberFretsProperty = DependencyProperty.Register(
            "NumberFrets", typeof (uint), typeof (Frets),
            new FrameworkPropertyMetadata((uint) 24, FrameworkPropertyMetadataOptions.AffectsRender));

        public uint NumberFrets
        {
            get { return (uint) GetValue(NumberFretsProperty); }
            set { SetValue(NumberFretsProperty, value); }
        }

        private double _firstThresholdThickness=4;

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(new Pen(Brushes.White, _firstThresholdThickness), new Point(-_firstThresholdThickness/2, 0), new Point(-_firstThresholdThickness/2, ActualHeight));
            var con = 17.817;
            List<double> points = new List<double>();
            double lasX = 0;

            for (int index = 0; index < NumberFrets; index++)
            {
                double x = lasX + (ActualWidth - lasX) / con;
                lasX = x;
                points.Add(x);
            }

            double scale = ActualWidth / points.Last();

            for (int index = 0; index < NumberFrets; index++)
            {
                Point strPoint = new Point(points[index]* scale, 0);
                Point endPoint = new Point(points[index]* scale, ActualHeight);
                drawingContext.DrawLine(new Pen(Brushes.Black, 2), strPoint, endPoint);
            }
        }
    }
}
