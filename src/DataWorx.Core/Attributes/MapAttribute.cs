using DataWorx.Core.Graph;
using DataWorx.Core.ObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MapAttribute : Attribute
    {
        public virtual IObjectBuilder CreateBuilder(Node node)
        {
            return new ObjectBuilder(node);
        }
    }
}
