using DataWorx.Core.SetBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataWorx.Core.Graph
{
    public class TypeConverter<T> : ExpandableObjectConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var types = Ject.FindAllAssignableFrom(typeof(T))
                            .Select(t => t.FullName)
                            .ToArray();

            return new StandardValuesCollection(types);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Activator.CreateInstance(Ject.FindType(value.ToString()));
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
