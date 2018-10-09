using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.Graph;

namespace DataWorx.Core.ObjectBuilders
{
    public class ObjectBuilder : IObjectBuilder
    {
        public int Count { get; set; }
        public Node Node { get; private set; }
        public IObjectBuilder Next { get; set; }

        public ObjectBuilder(Node node)
        {
            Node = node;
        }

        public virtual object Create()
        {
            object o;
            if (Creatables.Contains(Node.Type))
                o = Creatables.Get(Node.Type);
            else
                o = Activator.CreateInstance(Node.Type);
            return o;
        }

        public virtual object Map(object o)
        {
            // set simple properties
            foreach (FieldProperty property in Node.Properties)
                Ject.Set(o, property.Name, property.Builder.Create());

            if (Next != null)
                return Next.Map(o);
            else
                return o;
        }

        /// <summary>
        /// Create and map an object
        /// </summary>
        /// <returns></returns>
        public virtual object Generate()
        {
            // create instance
            var o = Create();
            // return mapped data
            return Map(o);
        }
    }
}
