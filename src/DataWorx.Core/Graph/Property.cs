using System;
using System.Linq;
using System.Reflection;
using DataWorx.Core.Math;
using DataWorx.Core.Attributes;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.Graph
{
    public class Property
    {
        public string Name { get; set; }

        public Type Type { get; set; }
        public static Property New(string name, Type type)
        {
            return new Property { Name = name, Type = type };
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name, Type);
        }
    }
}
