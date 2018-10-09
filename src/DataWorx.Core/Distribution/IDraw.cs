using System;
using System.Linq;
using DataWorx.Core.Math;
using System.Collections.Generic;

namespace DataWorx.Core.Distribution
{
    public interface IDraw
    {
        int Draw(int setcount);
    }
}