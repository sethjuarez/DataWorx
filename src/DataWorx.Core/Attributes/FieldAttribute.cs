using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;

namespace DataWorx.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldAttribute : Attribute
    {
        public abstract IFieldBuilder CreateFieldBuilder(Type type);
    }
}
