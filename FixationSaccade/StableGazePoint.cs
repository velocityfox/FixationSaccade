using Analytics.Maths.Timeseries;
using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixationSaccade
{
    public class StableGazePoint 
    {
        private IMovingAverage movingAverageX;
        private IMovingAverage movingAverageY;
        public double X { get { return movingAverageX.Average(); } }
        public double Y { get { return movingAverageY.Average(); } }
        public double Timestamp { get; private set; }
        public StableGazePoint(int length)
        {
            Length = length;
            movingAverageX = new SimpleMovingAverage(0, Length);
            movingAverageY = new SimpleMovingAverage(0, Length);
        }

        public int Length { get; }

        public void Add(GazePointEventArgs gazePoint)
        {
            movingAverageX.Progress(gazePoint.X);
            movingAverageY.Progress(gazePoint.Y);
            Timestamp = gazePoint.Timestamp;
        }
    }
}
