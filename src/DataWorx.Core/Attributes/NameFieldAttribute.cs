using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;

namespace DataWorx.Core.Attributes
{
    public class NameFieldAttribute : FieldAttribute
    {
        public NameFieldAttribute(int min, int max)
        {
            Max = max;
            Min = min;
        }
        public int Min { get; private set; }
        public int Max { get; private set; }

        public override IFieldBuilder CreateFieldBuilder(Type type)
        {
            if (type != typeof(string))
                throw new InvalidOperationException("Can only work with string types");
            return new NameFieldBuilder(Min, Max);
        }
    }
}
