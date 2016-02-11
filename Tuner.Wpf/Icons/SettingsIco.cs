using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuner.Wpf.Icons
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf.Icons"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tuner.Wpf.Icons;assembly=Tuner.Wpf.Icons"
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
    ///     <MyNamespace:Settings/>
    ///
    /// </summary>
    public class SettingsIco : Control
    {
        private  static Brush _defaultBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3b97d3"));
        static SettingsIco()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingsIco), new FrameworkPropertyMetadata(typeof(SettingsIco)));
        }

        public static readonly DependencyProperty InternalBorderThicknessProperty = DependencyProperty.Register(
            "InternalBorderThickness", typeof (double), typeof (SettingsIco), new PropertyMetadata(3.0));

        public double InternalBorderThickness
        {
            get { return (double) GetValue(InternalBorderThicknessProperty); }
            set { SetValue(InternalBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty InternalBorderStrokeProperty = DependencyProperty.Register(
            "InternalBorderStroke", typeof (Brush), typeof (SettingsIco), new PropertyMetadata(_defaultBrush));

        public Brush InternalBorderStroke
        {
            get { return (Brush) GetValue(InternalBorderStrokeProperty); }
            set { SetValue(InternalBorderStrokeProperty, value); }
        }

        public static readonly DependencyProperty InternalBorderFillProperty = DependencyProperty.Register(
            "InternalBorderFill", typeof (Brush), typeof (SettingsIco), new PropertyMetadata(Brushes.Transparent));

        public Brush InternalBorderFill
        {
            get { return (Brush)GetValue(InternalBorderFillProperty); }
            set { SetValue(InternalBorderFillProperty, value); }
        }

        public static readonly DependencyProperty ExternalBorderFillProperty = DependencyProperty.Register(
            "ExternalBorderFill", typeof (Brush), typeof (SettingsIco), new PropertyMetadata(default(Brush)));

        public Brush ExternalBorderFill
        {
            get { return (Brush) GetValue(ExternalBorderFillProperty); }
            set { SetValue(ExternalBorderFillProperty, value); }
        }

        public static readonly DependencyProperty ExternalBorderStrokeProperty = DependencyProperty.Register(
            "ExternalBorderStroke", typeof (Brush), typeof (SettingsIco), new PropertyMetadata(_defaultBrush));

        public Brush ExternalBorderStroke
        {
            get { return (Brush) GetValue(ExternalBorderStrokeProperty); }
            set { SetValue(ExternalBorderStrokeProperty, value); }
        }

        public static readonly DependencyProperty ExternalBorderThicknessProperty = DependencyProperty.Register(
            "ExternalBorderThickness", typeof (double), typeof (SettingsIco), new PropertyMetadata(8.0));

        public double ExternalBorderThickness
        {
            get { return (double) GetValue(ExternalBorderThicknessProperty); }
            set { SetValue(ExternalBorderThicknessProperty, value); }
        }
    }
}
