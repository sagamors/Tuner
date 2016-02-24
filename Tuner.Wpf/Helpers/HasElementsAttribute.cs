using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Tuner.Wpf.Helpers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HasElementsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count > 0;
            }
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return enumerable.GetEnumerator().MoveNext();
            }
            return false;
        }
    }
}
