using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuner.Wpf.Helpers
{
    public class ComboBoxTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplateSelector SelectedItemTemplateSelector { get; set; }
        public DataTemplate DropdownItemsTemplate { get; set; }
        public DataTemplateSelector DropdownItemsTemplateSelector { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var parent = container;

            while (parent != null && !(parent is ComboBoxItem) && !(parent is ComboBox))
                parent = VisualTreeHelper.GetParent(parent);

            var inDropDown = (parent is ComboBoxItem);

            return inDropDown
                ? DropdownItemsTemplate ?? DropdownItemsTemplateSelector?.SelectTemplate(item, container)
                : SelectedItemTemplate ?? SelectedItemTemplateSelector?.SelectTemplate(item, container);
        }
    }
}
