using System;
using System.Linq;
using DataWorx.Core.Math;
using System.Collections.Generic;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.SetBuilders
{
    public class BetweenSet : ISetBuilder
    {
        public IObjectBuilder Builder { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }

        public BetweenSet(IObjectBuilder builder, int min, int max)
        {
            Min = min;
            Max = max;
            Builder = builder;
        }

        public BetweenSet(IObjectBuilder builder, int max)
            : this(builder, 0, max)
        {

        }
        public BetweenSet()
        {
            
        }

        public IEnumerable<object> GetSet()
        {
            var count = Sampling.GetUniform(Min, Max);
            for (int i = 0; i < count; i++)
                yield return Builder.Generate();
        }
    }
}
