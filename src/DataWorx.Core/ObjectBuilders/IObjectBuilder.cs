using DataWorx.Core.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.ObjectBuilders
{
    public interface IObjectBuilder
    {
        object Create();
        object Map(object o);
        Node Node { get; }
        IObjectBuilder Next { get; set; }
        object Generate();
    }
}
