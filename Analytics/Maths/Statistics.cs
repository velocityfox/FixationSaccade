using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Maths.Statistics
{
    public static class Stats
    {
        public static double StandardDeviation(IEnumerable<double> population)
        {
            return MathNet.Numerics.Statistics.Statistics.StandardDeviation(population);
        }
    }
    public static class Probability
    {
        public static double StandardNormalPdf(double z)
        {
            return NormalPdf(z, 0, 1);
        }
        public static double NormalPdf(double z,double mean, double stdDev)
        {
            return 1 / Math.Sqrt(Math.PI * 2) * Math.Exp(-Math.Pow((z-mean)/stdDev,2)/ 2);
        }
    }
}
