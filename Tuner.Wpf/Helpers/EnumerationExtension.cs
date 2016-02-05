using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;

namespace Tuner.Wpf.Helpers
{
    public class EnumerationExtension : MarkupExtension
    {
        private Type _enumType;

        public EnumerationExtension(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");

            EnumType = enumType;
        }

        public Type EnumType
        {
            get { return _enumType; }
            private set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enumValues = Enum.GetValues(EnumType);

            return (
                from object enumValue in enumValues
                select new EnumerationMember
                {
                    Value = enumValue,
                    Description = GetDescription(enumValue),
                    DisplayName = GetDisplayName(enumValue)
                }).ToArray();
        }

        private string GetDescription(object enumValue)
        {
            var name = enumValue.ToString();
            var descriptionAttribute = EnumType
                .GetField(name)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute != null
                ? descriptionAttribute.Description
                : name;
        }

        private string GetDisplayName(object enumValue)
        {
            var name = enumValue.ToString();
            var nameAttribute = EnumType
                .GetField(name)
                .GetCustomAttributes(typeof(DisplayNameAttribute), false)
                .FirstOrDefault() as DisplayNameAttribute;
            return nameAttribute != null
                ? nameAttribute.DisplayName
                : name;
        }

        public class EnumerationMember
        {
            public string Description { get; set; }
            public string DisplayName { get; set; }
            public object Value { get; set; }
        }
    }
}
