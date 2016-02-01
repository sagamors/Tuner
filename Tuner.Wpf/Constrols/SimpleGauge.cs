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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf.Constrols"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf.Constrols;assembly=Tuner.Wpf.Constrols"
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
    ///     <MyNamespace:SimpleGauge/>
    ///
    /// </summary>
    public class SimpleGauge : Control
    {
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle", typeof (double), typeof (SimpleGauge), new PropertyMetadata(default(double)));

        public double StartAngle
        {
            get { return (double) GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
            "Angle", typeof (double), typeof (SimpleGauge), new PropertyMetadata(default(double)));

        public double Angle
        {
            get { return (double) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
    "Thickness", typeof(double), typeof(SimpleGauge), new PropertyMetadata(30.0));

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly DependencyProperty PointerLengthProperty = DependencyProperty.Register(
            "PointerLength", typeof (double), typeof (SimpleGauge), new PropertyMetadata(100.0));

        public double PointerLength
        {
            get { return (double) GetValue(PointerLengthProperty); }
            set { SetValue(PointerLengthProperty, value); }
        }

        static SimpleGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleGauge), new FrameworkPropertyMetadata(typeof(SimpleGauge)));
        }
    }
}
