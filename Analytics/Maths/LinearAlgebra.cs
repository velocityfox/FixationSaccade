using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Maths.LinearAlgebra
{
    public class VectorBuilder : List<double>
    {
        public VectorBuilder(IEnumerable<double> collection) : base(collection) { }

        public Vector<double> Build()
        {
            var v1 = MathNet.Numerics.LinearAlgebra.Double.Vector.Build.Dense(this.ToArray<double>());
            return v1;
        }
    }
    public static class VectorHelper
    {
        public static Vector<double> Pow(this Vector<double> vector, double exponent)
        {
            return vector.ElementWise(x => Math.Pow(x, exponent));
        }
        public static Vector<double> ElementWise(this Vector<double> vector, Func<double, double> mathsOperation)
        {
            return new VectorBuilder
            (
                vector.AsEnumerable().Select(x => mathsOperation(x)).ToArray()
            ).Build();
        }
        public static double EuclideanDistanceTo(this Vector<double> v1, Vector<double> v2)
        {
            return Math.Sqrt((v1 - v2).L2Norm());
        }
    }
}
