using System;
using System.Linq;
using System.Collections.Generic;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.SetBuilders
{
    public class StaticSet : ISetBuilder
    {
        public IObjectBuilder Builder { get; set; }
        public int Count { get; private set; }
        public StaticSet(IObjectBuilder builder, int count)
        {
            Count = count;
            Builder = builder;
        }

        public StaticSet()
        {
            Count = 10;
        }

        public IEnumerable<object> GetSet()
        {
            for (int i = 0; i < Count; i++)
            {
                var o = Builder.Generate();
                yield return o;
            }
        }
    }
}
