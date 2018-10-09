using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public interface IFieldBuilder
    {
        object Create();
    }
}
