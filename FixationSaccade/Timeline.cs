using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FixationSaccade
{
    public partial class Timeline : Form
    {
        private GazeAnalyser gazeAnalyser;
        public Form1 Form1 { get; private set; }

        public Timeline(Form1 form1)
        {
            InitializeComponent();
            this.Form1 = form1;
            this.gazeAnalyser = new GazeAnalyser();
            this.gazeAnalyser.OnMovementDataChanged += () =>
                {
                    this.microChannel.SetDataPoints(
                        gazeAnalyser.Movements.Select(m => new DataPoint(
                            m.Timestamp/60000, //ms to minutes
                            m.Distance))
                    );
                };
            this.microChannel.OnRangeSelectionChange += () =>
                {
                    movementChannel.SetDataPoints(
                        gazeAnalyser.Movements
                        .Where(m => m.Timestamp >= microChannel.SelectedRange.From*60000 && m.Timestamp <= microChannel.SelectedRange.To*60000)
                        .Select(m => new DataPoint(
                            m.Timestamp / 60000,
                            m.Distance))
                    );
                };
            this.gazeAnalyser.OnCalibratorChanged += calibrator =>
                {
                    textBoxScalingX.Text = calibrator.ScalingX.ToString();
                    textBoxScalingY.Text = calibrator.ScalingY.ToString();
                };
            this.gazeAnalyser.OnStableGazePointsChanged += DrawPoints;
            this.movementChannel.OnRangeSelectionChange += DrawPoints;
            this.gazeAnalyser.OnFixationDataChanged += DrawPoints;
            this.Resize += Timeline_SizeChanged;
        }
        private void DrawPoints()
        {
            var range = movementChannel.SelectedRange;
            Form1.ClearDrawings();
            //Convert minutes back to ms by *60000
            Func<double, bool> isTimestampInRange = t => t >= range.From * 60000 && t <= range.To * 60000;
            var start = Color.Blue;
            var end = Color.Red;
            var gazeEntries = gazeAnalyser.StableGazePoints.Where(p => isTimestampInRange(p.Timestamp));
            var steps = gazeEntries.Count() - 1;
            var increment = new double[] {
                (double)(end.A-start.A)/steps,
                (double)(end.R-start.R)/steps,
                (double)(end.G-start.G)/steps,
                (double)(end.B-start.B)/steps,
            };
            var myColor = new double[] { (double)start.A, (double)start.R, (double)start.G, (double)start.B };
            var firstRun = true;
            foreach (var gazeEntry in gazeEntries)
            {
                if (firstRun)
                    firstRun = false;
                else
                    for (int i = 0; i < 3; i++)
                        myColor[i] += increment[i];
                var calibratedPoint = gazeAnalyser.Calibrator.CalibrateGazePoint(new GazePoint(gazeEntry.X, gazeEntry.Y));
                Form1.DrawSquare((int)calibratedPoint.X, (int)calibratedPoint.Y, new SolidBrush(Color.FromArgb(
                    (byte)myColor[0],
                    (byte)myColor[1],
                    (byte)myColor[2],
                    (byte)myColor[3]
                    )), 3);
            }
            foreach (var fixation in gazeAnalyser.Fixations.Where(p => isTimestampInRange(p.Timestamp)))
            {
                if (Double.IsNaN(fixation.X) || Double.IsNaN(fixation.Y))
                    continue;
                var calibratedFixation = gazeAnalyser.Calibrator.CalibrateGazePoint(new GazePoint(fixation.X, fixation.Y));
                int width = (int)((double)fixation.Duration / 15D);
                width = width < 50 ? 50 : width;
                width = width > 50 ? 50 : width;
                Form1.DrawElipse((int)calibratedFixation.X, (int)calibratedFixation.Y, width, width, Brushes.LightSeaGreen);
            }
        }

        private void AnalyseExperiment_Click(object sender, EventArgs e)
        {
            gazeAnalyser.LoadExperiment(ExperimentId.Text);
            gazeAnalyser.SetSlidingWindowSize(int.Parse(textBoxSlidingWindowSize.Text));
        }

        private void buttonApplyCalibration_Click(object sender, EventArgs e)
        {
            gazeAnalyser.SetSlidingWindowSize(int.Parse(textBoxSlidingWindowSize.Text));
            gazeAnalyser.Calibrator.SetScaling(double.Parse(textBoxScalingX.Text), double.Parse(textBoxScalingY.Text));
            DrawPoints();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void Timeline_SizeChanged(object sender, System.EventArgs e)
        {
            var w = this.Width;
            movementChannel.Width = w - (movementChannel.Left)-24;
            microChannel.Width = w - (microChannel.Left)-24;
        }
    }
}
