using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuner.Wpf.Constrols
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
    ///     <MyNamespace:Cord/>
    ///
    /// </summary>
    public class Cord : Control
    {
        static Cord()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Cord), new FrameworkPropertyMetadata(typeof(Cord)));
        }

        public static readonly DependencyProperty CordStrokeProperty = DependencyProperty.Register(
            "CordStroke", typeof (Brush), typeof (Cord), new PropertyMetadata(default(Brush)));

        public Brush CordStroke
        {
            get { return (Brush) GetValue(CordStrokeProperty); }
            set { SetValue(CordStrokeProperty, value); }
        }
    }
}
