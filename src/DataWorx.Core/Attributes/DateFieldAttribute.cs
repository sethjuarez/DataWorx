using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;

namespace DataWorx.Core.Attributes
{
    public class DateFieldAttribute : FieldAttribute
    {
        public DateFieldAttribute(DateTime from, DateTime to)
        {
            To = to;
            From = from;
        }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public override IFieldBuilder CreateFieldBuilder(Type type)
        {
            if (type != typeof(DateTime))
                throw new InvalidOperationException("Can only work with DateTime types");
            return new DateTimeFieldBuilder(From, To);
        }
    }
}