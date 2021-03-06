﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tuner.Core;

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
    ///     <MyNamespace:TuneInfoControl/>
    ///
    /// </summary>
    public class TuneInfoControl : Control
    {
        private double _maxAngleValue = 67.5;
        private double _maxPercent = 37.5;
        private RotateTransform _rotateTransform;

        static TuneInfoControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TuneInfoControl), new FrameworkPropertyMetadata(typeof(TuneInfoControl)));
        }

        public override void OnApplyTemplate()
        {
            _rotateTransform = this.GetTemplateChild("PART_RotateTransform") as RotateTransform;
            base.OnApplyTemplate();

        }

        public static readonly DependencyProperty NearestNoteProperty = DependencyProperty.Register(
            "NearestNote", typeof (INote), typeof (TuneInfoControl), new PropertyMetadata(default(INote)));

        public INote NearestNote
        {
            get { return (INote) GetValue(NearestNoteProperty); }
            set { SetValue(NearestNoteProperty, value); }
        }

        public static readonly DependencyProperty FrequencyProperty = DependencyProperty.Register(
            "Frequency", typeof (double), typeof (TuneInfoControl), new PropertyMetadata(default(double), (o, args) =>
            {
                var control = o as TuneInfoControl;
                control.SetArrowAngle();
            }));

        public double Frequency
        {
            get { return (double) GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        public static readonly DependencyProperty TargetNoteProperty = DependencyProperty.Register(
            "TargetNote", typeof (INote), typeof (TuneInfoControl), new PropertyMetadata(default(INote)));

        public INote TargetNote
        {
            get { return (INote) GetValue(TargetNoteProperty); }
            set { SetValue(TargetNoteProperty, value); }
        }

        public double SpeedValueChange { get; set; } = 100;

        private void SetArrowAngle()
        {
            if(TargetNote==null) return;
            var percent =   (Frequency - TargetNote.Frequency) * 100  / TargetNote.Frequency;
            if (Math.Abs(percent) > _maxPercent)
            {
                percent = _maxPercent * Math.Sign(percent);
            }
            var value = (percent*_maxAngleValue)/_maxPercent;
            var animation = new DoubleAnimation(value, new Duration(TimeSpan.FromSeconds(Math.Abs(value / SpeedValueChange))));
            _rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }
    }
}
