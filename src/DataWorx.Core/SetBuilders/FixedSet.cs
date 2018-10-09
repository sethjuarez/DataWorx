using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.SetBuilders
{
    public class FixedSet : ISetBuilder
    {
        public IObjectBuilder Builder { get; set; }
        public string Property { get; private set; }
        public object[] Set { get; private set; }

        public FixedSet(IObjectBuilder builder, string property, params object[] args)
        {
            Set = args;
            Property = property;
            Builder = builder;
        }

        public FixedSet()
        {
            
        }

        public IEnumerable<object> GetSet()
        {
            for (int i = 0; i < Set.Length; i++)
            {
                var o = Builder.Generate();
                Ject.Set(o, Property, Set[i]);
                yield return o;
            }
        }
    }
}