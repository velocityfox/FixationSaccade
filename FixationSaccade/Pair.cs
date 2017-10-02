using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixationSaccade
{
    public class Pair<T>
    {
        public Pair(T x, T y)
        {
            X = x;
            Y = y;
        }

        public T X { get; }
        public T Y { get; }
    }
}
