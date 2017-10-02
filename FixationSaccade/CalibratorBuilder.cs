using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace FixationSaccade
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; }
        public double Y { get; }
    }
    public class GazePoint : Point
    {
        public GazePoint(double x, double y) : base(x, y)
        {
        }
    }
    public class CalibrationPoint : Point
    {
        public CalibrationPoint(double x, double y) : base(x, y)
        {
        }
    }
    public class CalibratedPoint : Point
    {
        public CalibratedPoint(double x, double y) : base(x, y)
        {
        }
    }

    public class CalibratorBuilder
    {
        const double TOLERANCE=0.01;
        public List<CalibrationPoint> Points { get; private set; }
        Dictionary<CalibrationPoint, List<GazePointEventArgs>> samples;
        private readonly Rectangle area;
        private readonly Strategy strategy;

        public enum Strategy
        {
            Simple4,
            DieFace5,
            Square9,
            VerticalZigZag6
        }
        public CalibratorBuilder(Rectangle area,Strategy strategy)
        {
            double screenWidthPixels = area.Width;
            double screenHeightPixels = area.Height;
            Points = new List<CalibrationPoint>();
            switch (strategy)
            {
                case Strategy.Square9:
                    double quarter = 1.0 / 4.0;
                    for (double x = quarter; x < .999; x += quarter)
                        for (double y = quarter; y < .999; y += quarter)
                            Points.Add(new CalibrationPoint(
                                x * screenWidthPixels + area.Left,
                                y * screenHeightPixels + area.Top));
                    break;
                case Strategy.DieFace5:
                    Points.Add(new CalibrationPoint(
                        .5 * screenWidthPixels + area.Left,
                        .5 * screenHeightPixels + area.Top));
                    goto Simple4;
                case Strategy.Simple4:
                    Simple4:
                    double quotient = 1.0 / 3.0;
                    for (double x = quotient; x < .999; x += quotient)
                        for (double y = quotient; y < .999; y += quotient)
                            Points.Add(new CalibrationPoint(
                                x * screenWidthPixels + area.Left,
                                y * screenHeightPixels + area.Top));
                    break;
                case Strategy.VerticalZigZag6:
                    var th7 = 1.0 / 7.0;
                    for (int i = 1; i < 7; i++)
                    {
                        Points.Add(new CalibrationPoint(
                            (i % 2 == 1 ? .25 : .75) * screenWidthPixels + area.Left,   //alternate x between left and right sides of screen
                            i * th7 * screenHeightPixels + area.Top));                  //do this for size rows
                    }
                    break;
            }
            samples = new Dictionary<CalibrationPoint, List<GazePointEventArgs>>();
            foreach (var point in Points)
            {
                samples.Add(point, new List<GazePointEventArgs>());
            }

            this.area = area;
            this.strategy = strategy;
        }
        public bool IsMoreCalibrationNeeded(CalibrationPoint point)
        {
            var sample = samples[point];
            return sample.Count() <= 100; 
                //|| Maths.Statistics.Stats.StandardDeviation(sample.Select(c=>c.X)) > TOLERANCE
                //|| Maths.Statistics.Stats.StandardDeviation(sample.Select(c=>c.Y)) > TOLERANCE;
        }
        public void AddCalibration(CalibrationPoint point, GazePointEventArgs gaze)
        {
            if (!samples.Keys.Contains(point))
                throw new ArgumentException("Calibration point given is not part of calibration set.");
            samples[point].Add(gaze);
        }
        public Calibrator BuildCalibratedScreen()
        {
            return new GlobalScalingLocalOffsetCalibrator(samples.Select(kvp => new KeyValuePair<GazePoint, CalibrationPoint>(
                new GazePoint(kvp.Value.Select(gp => gp.X).Average(), kvp.Value.Select(gp => gp.Y).Average()),
                kvp.Key))
                .ToDictionary(kv=>kv.Key,kv=>kv.Value));
        }
    }
}
