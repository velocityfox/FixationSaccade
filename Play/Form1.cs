using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;

namespace Play
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var v1 = new VectorBuilder { 1, 2, 3 }.Build();
            //var v2 = new VectorBuilder { 3, 2, 1 }.Build();
            //var v3 = (v1-v2);
            //var v4 = v3.ElementWise(x => Math.Pow(x, 2));
            //MessageBox.Show(v4.ToString());
        }
    }
    //public class Vector : MathNet.Numerics.LinearAlgebra.Double.Vector
    //{
    //    public Vector(VectorStorage<double> storage) : base(storage)
    //    {
    //    }

    //    public Vector ElementWise(Func<double,double> mathsOperation)
    //    {
    //        return (Vector) MathNet.Numerics.LinearAlgebra.Double.Vector.Build.Dense(this.AsEnumerable().Select(x=>mathsOperation(x)).ToArray());
    //    }
    //}
}
