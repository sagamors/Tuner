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
using Tuner.Wpf.Sound;

namespace Tuner.Wpf.Views
{
    /// <summary>
    /// Interaction logic for TuningsView.xaml
    /// </summary>
    public partial class TuningsView : UserControl
    {
        public TuningsView()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            //if (PresetsCombobox.IsKeyboardFocusWithin)
            //{
            //    PresetsCombobox.StaysOpenOnEdit = true;
            //    PresetsCombobox.IsDropDownOpen = true;
            //}
            //PresetsCombobox.Items.Filter = o =>
            //{
            //    var selectedPreset = PresetsCombobox.SelectedValue as IPreset;
            //    if (selectedPreset!=null && selectedPreset.Name.ToUpper() == PresetsCombobox.Text.ToUpper())
            //    {
            //        PresetsCombobox.StaysOpenOnEdit = false;
            //        PresetsCombobox.IsDropDownOpen = false;
            //        return true;
            //    }
            //    if (!PresetsCombobox.IsKeyboardFocusWithin) return true;
            //    IPreset preset = o as IPreset;
            //    if (preset == null) return true;

            //    return preset.Name.Contains(PresetsCombobox.Text);
            //};
        }
    }
}
