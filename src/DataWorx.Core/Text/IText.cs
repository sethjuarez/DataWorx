using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Text
{
    public interface IText
    {
        string Create(int min, int max);
        string Create(int length);
    }
}
