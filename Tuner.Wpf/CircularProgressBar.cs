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

namespace Tuner.Wpf
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf;assembly=Tuner.Wpf"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CircularProgressBar/>
    ///
    /// </summary>
    public class CircularProgressBar : ContentControl
    {
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle", typeof (double), typeof (CircularProgressBar), new PropertyMetadata(default(double)));

        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (double), typeof (CircularProgressBar), new PropertyMetadata(default(double)));

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ProgressBrushProperty = DependencyProperty.Register(
            "ProgressBrush", typeof (Brush), typeof (CircularProgressBar), new PropertyMetadata(Brushes.Black));

        public Brush ProgressBrush
        {
            get { return (Brush) GetValue(ProgressBrushProperty); }
            set { SetValue(ProgressBrushProperty, value); }
        }

        public static readonly DependencyProperty ProgressThicknessProperty = DependencyProperty.Register(
            "ProgressThickness", typeof (double), typeof (CircularProgressBar), new PropertyMetadata(1.0));

        public double ProgressThickness
        {
            get { return (double) GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }

        public static readonly DependencyProperty ProgressBackgroundProperty = DependencyProperty.Register(
            "ProgressBackground", typeof (Brush), typeof (CircularProgressBar), new PropertyMetadata(Brushes.CornflowerBlue));

        public Brush ProgressBackground
        {
            get { return (Brush) GetValue(ProgressBackgroundProperty); }
            set { SetValue(ProgressBackgroundProperty, value); }
        }

        static CircularProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgressBar), new FrameworkPropertyMetadata(typeof(CircularProgressBar)));
        }
    }
}
