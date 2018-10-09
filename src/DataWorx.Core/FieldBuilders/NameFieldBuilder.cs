using DataWorx.Core.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public class NameFieldBuilder : IFieldBuilder
    {
        private int Min { get; set; }
        public int Max { get; set; }

        private readonly NameText _text;
        public NameFieldBuilder(int min = 5, int max = 10)
        {
            _text = new NameText();
            Max = max;
            Min = min;
        }

        public object Create()
        {
            return _text.Create(Min, Max);
        }
    }
}
