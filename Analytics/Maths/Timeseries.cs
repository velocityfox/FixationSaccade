using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Maths.Timeseries
{
    public interface IMovingAverage
    {
        void Progress(double nextValue);
        double Average();
    }
    public class SimpleMovingAverage : IMovingAverage
    {
        Queue<double> values;
        public SimpleMovingAverage(double initialValue,int length)
        {
            values = new Queue<double>();
            Length = length;
            Progress(initialValue);
        }

        public int Length { get; }

        public double Average()
        {
            return values.Average();
        }

        public void Progress(double nextValue)
        {
            while(values.Count()>=Length)
            {
                values.Dequeue();
            }
            values.Enqueue(nextValue);
        }
    }
}
