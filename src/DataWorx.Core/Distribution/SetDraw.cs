using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Distribution
{
    public class SetDraw<T>
    {
        private readonly IEnumerable<T> _list;
        private readonly IDraw _draw;
        public SetDraw(IEnumerable<T> list, IDraw draw)
        {
            _draw = draw;
            _list = list;
        }

        public T Draw()
        {
            int count = _list.Count();
            var idx = _draw.Draw(count);
            if (idx >= 0 && idx < count)
                return _list.ElementAt(idx);
            else
                throw new IndexOutOfRangeException();
        }
    }
}
