using DataWorx.Core.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.ObjectBuilders
{
    public class StockEntity
    {
        public decimal Last { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public long Volume { get; set; }
    }

    public class StockBuilder : ObjectBuilder
    {
        public StockBuilder(Node node)
            : base(node)
        {

        }

        public override object Map(object o)
        {
            if (Next != null)
                return Next.Map(o);
            else
                return o;
        }
    }
}
