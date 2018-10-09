using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWorx.Core.FieldBuilders
{
    public class BoolFieldBuilder : IFieldBuilder
    {
        public object Create()
        {
            return Sampling.GetUniform() < .501;
        }
    }
}
