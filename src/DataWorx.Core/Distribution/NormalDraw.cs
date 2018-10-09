using System;
using System.Linq;
using DataWorx.Core.Math;
using System.Collections.Generic;

namespace DataWorx.Core.Distribution
{
    public class NormalDraw : IDraw
    {
        public int Draw(int setcount)
        {
            return NormalDraw.Get(setcount);
        }

        public static int Get(int count)
        {
            if (count == 0) throw new IndexOutOfRangeException("Cannot draw from an empty set");
            var mean = (count - 1) / 2d;
            var stddev = System.Math.Sqrt((count * (count + 1)) / 12d);
            var index = (int)System.Math.Round(Sampling.GetNormal(mean, stddev), 0);

            // on the off chance we are out of bounds...
            // (it is not likely)
            while (index < 0 || index >= count)
                index = (int)System.Math.Round(Sampling.GetNormal(mean, stddev), 0);

            return index;
        }
    }
}
