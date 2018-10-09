using System;
using System.Linq;
using DataWorx.Core.Math;
using System.ComponentModel;
using System.Collections.Generic;

namespace DataWorx.Core.FieldBuilders
{
    public class NumberFieldBuilder : IFieldBuilder
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public Type NumericType { get; set; }
        public NumberFieldBuilder()
        {
            // TODO: Should also specify distribution
            NumericType = typeof(double);
            Max = 1;
            Min = 0;
        }

        private double GetRandom(double min, double max)
        {
            return min + (int)(Sampling.GetUniform() * ((max - min) + 1));
        }

        public object Create()
        {
            var number = GetRandom(Min, Max);

            if (NumericType == typeof(decimal))
                return Convert.ToDecimal(number);

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
            if (converter.CanConvertTo(NumericType))
                return converter.ConvertTo(number, NumericType);
            else
                throw new InvalidCastException(string.Format("Cannot convert {0} to {1}", number, NumericType.Name));
        }
    }
}
