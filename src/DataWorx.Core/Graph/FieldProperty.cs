using System;
using System.Linq;
using System.Reflection;
using DataWorx.Core.Math;
using DataWorx.Core.Attributes;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;
using DataWorx.Core.ObjectBuilders;
using System.ComponentModel;

namespace DataWorx.Core.Graph
{
    public class FieldProperty : Property
    {
        [TypeConverter(typeof(TypeConverter<IFieldBuilder>))]
        public IFieldBuilder Builder { get; set; }
        public static FieldProperty New(string name, Type type, IFieldBuilder builder)
        {
            return new FieldProperty { Name = name, Type = type, Builder = builder };
        }
    }
}
