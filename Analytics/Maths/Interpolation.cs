using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Maths.Interpolation
{
    public static class Surfaces
    {
        public enum SurfaceFitting2DType
        {
            KernelSmoothing,
            Bilinear,
            Default
        }
        public interface ISurface2D
        {
            double Interpolate(double x, double y);
        }
        public class CalibrationPoint3d
        {
            public CalibrationPoint3d(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public double X { get; }
            public double Y { get; }
            public double Z { get; }
        }
        //The identity surface
        private class ZeroSurface : ISurface2D
        {
            public double Interpolate(double x, double y)
            {
                return 0;
            }
        }
        //Build a kerel smoothing fitted surface
        private class GuassianKernelSmoothingSurface2D : ISurface2D
        {
            private readonly List<CalibrationPoint3d> calibrationPoints;
            private readonly double radius;

            public GuassianKernelSmoothingSurface2D(List<CalibrationPoint3d> calibrationPoints, double radius)
            {
                this.calibrationPoints = calibrationPoints;
                this.radius = radius;
            }
            public double Interpolate(double x, double y)
            {
                double numerator=0d, denominator=0d;
                foreach (var calibrationPoint in calibrationPoints)
                {
                    double dist = Math.Sqrt(
                        Math.Pow((x - calibrationPoint.X), 2) 
                        + Math.Pow((y - calibrationPoint.Y), 2)
                    );
                    numerator += Statistics.Probability.NormalPdf(dist, 0, radius) * calibrationPoint.Z;
                    denominator += Statistics.Probability.NormalPdf(dist, 0, radius);
                }
                return numerator / denominator;
            }
        }
        private class BilinearSurface2D : ISurface2D
        {
            private readonly IEnumerable<CalibrationPoint3d> calibrationGrid;
            private List<double> xValues = new List<double>();
            private List<double> yValues = new List<double>();
            private Dictionary<double, Dictionary<double, double>> xyMap = new Dictionary<double, Dictionary<double, double>>();

            public BilinearSurface2D(IEnumerable<CalibrationPoint3d> calibrationGrid)
            {
                this.calibrationGrid = calibrationGrid;
                foreach (var p in calibrationGrid)
                {
                    if (!xyMap.ContainsKey(p.X))
                        xyMap.Add(p.X, new Dictionary<double, double>());
                    xyMap[p.X].Add(p.Y, p.Z);
                }
                xValues = xyMap.Keys.ToList();
                xValues.Sort();
                yValues = xyMap.Values.First().Keys.ToList();
                yValues.Sort();
            }
            public double Interpolate(double x, double y)
            {
                if (x < xValues[0])
                    return Interpolate(xValues[0], y);
                if (x > xValues.Last())
                    return Interpolate(xValues.Last(), y);
                if (y < yValues[0])
                    return Interpolate(x, yValues[0]);
                if (y > yValues.Last())
                    return Interpolate(x, yValues.Last());
                Func<double,double,double,double,double,double> interp = (v,x1,y1,x2,y2) => 
                    (v-x1)/(x2-x1)*(y2-y1)+y1;
                var yLower = yValues.Where(v => v <= y).Last();
                var yUpper = yValues.Where(v => v >= y).First(); 
                var xLower = xValues.Where(v => v <= x).Last();
                var xUpper = xValues.Where(v => v >= x).First();
                var zLower = 0d;
                var zUpper = 0d;
                if (xLower != xUpper)
                {
                    zLower = interp(x, xLower, xyMap[xLower][yLower], xUpper, xyMap[xUpper][yLower]);
                    zUpper = interp(x, xLower, xyMap[xLower][yUpper], xUpper, xyMap[xUpper][yUpper]);
                }
                else
                {
                    zLower = xyMap[xLower][yLower];
                    zUpper = xyMap[xLower][yUpper];
                }
                if (yLower != yUpper)
                    return interp(y, yLower, zLower, yUpper, zUpper);
                else
                    return zLower;
            }
        }
        public static ISurface2D CreateKernelSmoothingFittedSurface2D(SurfaceFitting2DType surfaceFitting, IEnumerable<CalibrationPoint3d> calibrationPoints, double radius)
        {
            if (surfaceFitting != SurfaceFitting2DType.KernelSmoothing)
            {
                throw new NotImplementedException("This function only knows about KernelSmoothing.");
            }

            return new GuassianKernelSmoothingSurface2D(
                calibrationPoints.ToList(), radius
                );
        }
        public static ISurface2D CreateBilinearInterpolatedSurface2D(IEnumerable<CalibrationPoint3d> calibrationGrid)
        {
            return new BilinearSurface2D(calibrationGrid);
        }
        public static ISurface2D CreateZeroSurface2D()
        {
            return new ZeroSurface();
        }
    }
}

