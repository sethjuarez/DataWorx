using DataWorx.Core.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public class StringFieldBuilder : IFieldBuilder
    {
        private static LongText _generator;
        public int Length  { get; set; }
        public StringFieldBuilder()
        {
            Length = 100;
        }

        public object Create()
        {
            if (_generator == null)
            {
                _generator = new LongText();
                _generator.Load();
            }

            return _generator.Create(Length);
        }
    }
}
