using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;

namespace DataWorx.Core.Attributes
{
    public class PhoneFieldAttribute : FieldAttribute
    {
        public override IFieldBuilder CreateFieldBuilder(Type type)
        {
            if (type != typeof(string))
                throw new InvalidOperationException("Can only work with DateTime types");
            return new PhoneFieldBuilder();
        }
    }
}
