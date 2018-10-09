using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public class PhoneFieldBuilder : IFieldBuilder
    {
        public object Create()
        {
            return String.Format("{0:(###) ###-####}", (long)(Sampling.GetUniform() * 10000000000));
        }
    }
}
