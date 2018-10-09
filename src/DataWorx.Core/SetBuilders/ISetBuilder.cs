using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.ObjectBuilders;
using System.ComponentModel;
using DataWorx.Core.Graph;

namespace DataWorx.Core.SetBuilders
{
    public interface ISetBuilder
    {
        [TypeConverter(typeof(TypeConverter<IObjectBuilder>))]
        IObjectBuilder Builder { get; set; }
        IEnumerable<object> GetSet();
    }
}