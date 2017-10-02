using Analytics.Maths.Interpolation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FixationSaccade
{
    public abstract class Calibrator
    {
        protected readonly Dictionary<GazePoint, CalibrationPoint> calibrationMapping;

        public Calibrator(Dictionary<GazePoint, CalibrationPoint> calibrationMapping)
        {
            this.calibrationMapping = calibrationMapping;
        }
        public static Dictionary<GazePoint, CalibrationPoint> DeserialiseCalibrationMapping(string calibratorAsString)
        {
            var cm = new Dictionary<GazePoint, CalibrationPoint>();
            using (var sr = new StringReader(calibratorAsString))
            {
                sr.ReadLine();//Discard scaling as we shall recalculate later.
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var coords = line.Split(',').Select(Double.Parse).ToList();
                    cm.Add(new GazePoint(coords[0], coords[1]), new CalibrationPoint(coords[2], coords[3]));
                }
            }
            return cm;
        }
        public abstract CalibratedPoint CalibrateGazePoint(GazePoint gaze);
        override public string ToString()
        {
            var sb = new StringBuilder();
            foreach (var cm in calibrationMapping)
            {
                sb.Append(cm.Key.X.ToString() + ",");
                sb.Append(cm.Key.Y.ToString() + ",");
                sb.Append(cm.Value.X.ToString() + ",");
                sb.AppendLine(cm.Value.Y.ToString());
            }
            return sb.ToString();
        }
    }
    public class ConstantCalibrator : Calibrator
    {
        private Surfaces.ISurface2D xSurface;
        private Surfaces.ISurface2D ySurface;
        public ConstantCalibrator(Dictionary<GazePoint, CalibrationPoint> calibrationMapping)
            : base(calibrationMapping)
        {
            double radius = 200;
            xSurface = Surfaces.CreateKernelSmoothingFittedSurface2D(
                Surfaces.SurfaceFitting2DType.KernelSmoothing,
                calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.X)),
                radius);
            ySurface = Surfaces.CreateKernelSmoothingFittedSurface2D(
                Surfaces.SurfaceFitting2DType.KernelSmoothing,
                calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.Y)),
                radius);
        }
        override public CalibratedPoint CalibrateGazePoint(GazePoint gaze)
        {
            return new CalibratedPoint(
                xSurface.Interpolate(gaze.X, gaze.Y),
                ySurface.Interpolate(gaze.X, gaze.Y)
                );
        }
    }
    public class GlobalScalingLocalOffsetCalibrator : Calibrator
    {
        public void SetScaling(double x,double y)
        {
            meanScalingX = x;
            meanScalingY = y;
        }
        public double ScalingX { get { return meanScalingX; } }
        public double ScalingY { get { return meanScalingY; } }
        private Surfaces.ISurface2D xOffsetSurface;
        private Surfaces.ISurface2D yOffsetSurface;
        private double meanScalingX;
        private double meanScalingY;
        public GlobalScalingLocalOffsetCalibrator(Dictionary<GazePoint, CalibrationPoint> calibrationMapping)
            : base(calibrationMapping)
        {

            //Assume most of the projection can be explained globally by stretching the screen in both x and y directions separately. 
            double meanGazeX = calibrationMapping.Select(kvp => kvp.Key.X).Average();
            meanScalingX = calibrationMapping
               .Where(kvp => kvp.Key.X >= meanGazeX * .01)   //Ignore any points too close to the left side of the screen for X scaling
               .Select(kvp => kvp.Value.X / kvp.Key.X)       //Get ratio
               .Average();
            double meanGazeY = calibrationMapping.Select(kvp => kvp.Key.Y).Average();
            meanScalingY = calibrationMapping
               .Where(kvp => kvp.Key.Y >= meanGazeY * .01)   //Ignore any points too close to the top side of the screen for Y scaling
               .Select(kvp => kvp.Value.Y / kvp.Key.Y)       //Get ratio
               .Average();

            //Then model the offset that needs to be applied to each of the calibration points as a local phenomenon.
            if (false)
            {
                double radius = 2000; //pixels
                xOffsetSurface = Surfaces.CreateKernelSmoothingFittedSurface2D(
                    Surfaces.SurfaceFitting2DType.KernelSmoothing,
                    calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.X - kvp.Key.X * meanScalingX)),
                    radius);
                yOffsetSurface = Surfaces.CreateKernelSmoothingFittedSurface2D(
                    Surfaces.SurfaceFitting2DType.KernelSmoothing,
                    calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.Y - kvp.Key.Y * meanScalingY)),
                    radius);
            }
            else if(false)
            {
                xOffsetSurface = Surfaces.CreateBilinearInterpolatedSurface2D(
                    calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.X - kvp.Key.X * meanScalingX))
                    );
                yOffsetSurface = Surfaces.CreateBilinearInterpolatedSurface2D(
                    calibrationMapping.Select(kvp => new Surfaces.CalibrationPoint3d(kvp.Key.X, kvp.Key.Y, kvp.Value.Y - kvp.Key.Y * meanScalingY))
                    );
            }
            else
            {
                xOffsetSurface = Surfaces.CreateZeroSurface2D();
                yOffsetSurface = Surfaces.CreateZeroSurface2D();
            }
        }
        override public CalibratedPoint CalibrateGazePoint(GazePoint gaze)
        {
            return new CalibratedPoint(
                xOffsetSurface.Interpolate(gaze.X, gaze.Y)+gaze.X*meanScalingX,
                yOffsetSurface.Interpolate(gaze.X, gaze.Y)+gaze.Y*meanScalingY
                );
        }
        public override string ToString()
        {
            return meanScalingX.ToString()+","+meanScalingY+",1.0,1.0"+Environment.NewLine
                +base.ToString();
        }
    }
    public class ShiftedScaledCalibrator : Calibrator
    {
        public ShiftedScaledCalibrator(Dictionary<GazePoint, CalibrationPoint> calibrationMapping)
            : base(calibrationMapping)
        {
        }

        public override CalibratedPoint CalibrateGazePoint(GazePoint gaze)
        {
            throw new NotImplementedException();
        }
    }

}
