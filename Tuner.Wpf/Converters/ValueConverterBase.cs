using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tuner.Wpf.Converters
{
    public abstract class ValueConverterBase<TSource, TResult> : IValueConverter
    {
        public abstract object Convert(TSource value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(TResult value, Type targetType, object parameter, CultureInfo culture);
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resultType = typeof(TResult);
            var concrete = (TSource)System.Convert.ChangeType(value, typeof(TSource));
            if (resultType.IsAssignableFrom(targetType) ^ resultType == targetType) throw new Exception("");
            return Convert(concrete, targetType, parameter, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resultType = typeof(TSource);
            var concrete = (TResult)System.Convert.ChangeType(value, typeof(TResult));
            if (resultType.IsAssignableFrom(targetType) ^ resultType == targetType) throw new Exception("");
            return ConvertBack(concrete, targetType, parameter, culture);
        }
    }
}
