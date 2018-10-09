using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public class DateTimeFieldBuilder : IFieldBuilder
    {
        private readonly DateTime _from;
        private readonly DateTime _to;

        public DateTimeFieldBuilder()
        {
            _from = DateTime.Now.AddYears(-1);
            _to = DateTime.Now;
        }

        public DateTimeFieldBuilder(DateTime from, DateTime to)
        {
            _to = to;
            _from = from;
        }

        public object Create()
        {
            var span = _to - _from;
            var hrs = Sampling.GetUniform((int)System.Math.Floor(span.TotalHours - 1));
            return _from.AddHours(hrs);
        }
    }
}
