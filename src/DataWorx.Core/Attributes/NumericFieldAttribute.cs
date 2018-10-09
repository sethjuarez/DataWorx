using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;

namespace DataWorx.Core.Attributes
{
    public class NumericFieldAttribute : FieldAttribute
    {
        public NumericFieldAttribute(double min, double max)
        {
            Max = max;
            Min = min;
        }

        public double Min { get; private set; }
        public double Max { get; private set; }

        public override IFieldBuilder CreateFieldBuilder(Type type)
        {
            return new NumberFieldBuilder { NumericType = type, Min = Min, Max = Max };
        }
    }
}
