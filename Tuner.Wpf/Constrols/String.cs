using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
    ///     <MyNamespace:String/>
    ///
    /// </summary>
    public class String : ButtonBase
    {
        static String()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(String), new FrameworkPropertyMetadata(typeof(String)));
        }

        public override void OnApplyTemplate()
        {
            _partString = GetTemplateChild("PART_String") as Border;
            base.OnApplyTemplate();
        }

        public static readonly DependencyProperty SProperty = DependencyProperty.Register(
            "S", typeof (string), typeof (String), new PropertyMetadata(default(string)));


        public static readonly DependencyProperty StringNameProperty = DependencyProperty.Register(
            "StringName", typeof (string), typeof (String), new PropertyMetadata(default(string)));

        public string StringName
        {
            get { return (string) GetValue(StringNameProperty); }
            set { SetValue(StringNameProperty, value); }
        }

        public static readonly DependencyProperty StringBrushProperty = DependencyProperty.Register(
            "StringBrush", typeof (Brush), typeof (String), new PropertyMetadata(default(Brush)));

        public Brush StringBrush
        {
            get { return (Brush) GetValue(StringBrushProperty); }
            set { SetValue(StringBrushProperty, value); }
        }

        public static readonly DependencyProperty StringBorderThicknessProperty = DependencyProperty.Register(
            "StringBorderThickness", typeof (double), typeof (String), new PropertyMetadata(default(double)));

        public double StringBorderThickness
        {
            get { return (double) GetValue(StringBorderThicknessProperty); }
            set { SetValue(StringBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty StringThicknessProperty = DependencyProperty.Register(
            "StringThickness", typeof (double), typeof (String), new PropertyMetadata(default(double)));

        public double StringThickness
        {
            get { return (double) GetValue(StringThicknessProperty); }
            set { SetValue(StringThicknessProperty, value); }
        }

        public static readonly DependencyProperty StringBackgroundProperty = DependencyProperty.Register(
            "StringBackground", typeof (Brush), typeof (String), new PropertyMetadata(default(Brush)));

        public Brush StringBackground
        {
            get { return (Brush) GetValue(StringBackgroundProperty); }
            set { SetValue(StringBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(
            "PropertyType", typeof (Brush), typeof (String), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty StringNameStyleProperty = DependencyProperty.Register(
            "StringNameStyle", typeof (Style), typeof (String), new PropertyMetadata(default(Style)));

        public Style StringNameStyle
        {
            get { return (Style) GetValue(StringNameStyleProperty); }
            set { SetValue(StringNameStyleProperty, value); }
        }

        public static readonly DependencyProperty IsPlayProperty = DependencyProperty.Register(
            "IsPlay", typeof (bool), typeof (String), new PropertyMetadata(default(bool)));

        public bool IsPlay
        {
            get { return (bool) GetValue(IsPlayProperty); }
            set { SetValue(IsPlayProperty, value); }
        }

        public static readonly DependencyProperty AmplitudeProperty = DependencyProperty.Register(
            "Amplitude", typeof (double), typeof (String), new PropertyMetadata(5.0, (o, args) =>
            {
                var control = o as String;
                control.SetFromAmp();
            }));

        private Border _partString;

        public double Amplitude
        {
            get { return (double) GetValue(AmplitudeProperty); }
            set { SetValue(AmplitudeProperty, value); }
        }

        private void SetFromAmp()
        {
            var story = this._partString.FindResource("Storyboard")as Storyboard;
            story.Stop(_partString);
            var g = story.Children.OfType<DoubleAnimation>().FirstOrDefault();
            g.To = Amplitude;
            g.From = -Amplitude;
            _partString.BeginStoryboard(story);
        }
    }
}
