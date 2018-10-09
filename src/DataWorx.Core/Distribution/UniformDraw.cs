using System;
using System.Linq;
using DataWorx.Core.Math;
using System.Collections.Generic;

namespace DataWorx.Core.Distribution
{
    public class UniformDraw : IDraw
    {
        public int Draw(int setcount)
        {
            if (setcount == 0) throw new IndexOutOfRangeException("Cannot draw from an empty set");
            return Sampling.GetUniform(1, setcount) - 1;
        }
    }
}
