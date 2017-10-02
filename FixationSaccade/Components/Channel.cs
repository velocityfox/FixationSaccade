using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FixationSaccade.Components
{
    public class Channel : Chart
    {
        public Range SelectedRange { get; private set; }
        public class Range
        {
            public double From { get; private set; }
            public double To { get; private set; }
            public Range(double from, double to)
            {
                this.From = from;
                this.To = to;
            }
        }
        public event Action OnRangeSelectionChange;
        private Series dataSeries;
        private Series highlightSeries;
        private MouseEventArgs chartMouseDownEvent;

        public Channel() : base()
        {
            BackColor = Color.Black;
            this.dataSeries = new Series();
            this.highlightSeries = new Series();
            highlightSeries.ChartType = SeriesChartType.Line;
            highlightSeries.Color = Color.Yellow;
            highlightSeries.BorderColor = Color.Yellow;
            highlightSeries.BorderWidth = 5;
            base.Series.Add(dataSeries);
            base.Series.Add(highlightSeries);
            var chartArea = new ChartArea();
            chartArea.AxisY.Enabled = AxisEnabled.False;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Interval = 1;
            chartArea.BackColor = Color.Black;
            chartArea.AxisX.LabelStyle.ForeColor = Color.CornflowerBlue;
            chartArea.Position = new ElementPosition(0f, 0f, 100f, 100f);
            this.ChartAreas.Add(chartArea);
            this.SelectedRange = new Range(0, 0);
            this.MouseDown += (o, e) =>
            {
                highlightSeries.Points.Clear();
                this.chartMouseDownEvent = e;
            };
            this.MouseMove += (o, e) =>
            {
                if (e.Button != MouseButtons.Left)
                    return;
                if (this.chartMouseDownEvent != null)
                {
                    var axis = this.ChartAreas[0].AxisX;
                    var plotPosition = chartArea.InnerPlotPosition;
                    highlightSeries.Points.Clear();
                    Func<Double, Double> getX = x => (x/this.Width*100f-plotPosition.X) / (plotPosition.Width) * (axis.Maximum - axis.Minimum)+axis.Minimum;
                    var from = getX(this.chartMouseDownEvent.X);
                    var to = getX(e.X);
                    highlightSeries.Points.Add(new DataPoint(from, 0));
                    highlightSeries.Points.Add(new DataPoint(to, 0));
                    SelectedRange = new Range(Math.Min(from, to), Math.Max(from, to));
                }
            };
            this.MouseUp += (o, e) =>
            {
                OnRangeSelectionChange.Invoke();
            };
        }
        public void SetDataPoints(IEnumerable<DataPoint> points)
        {
            if (points.Count() == 0)
                return;
            dataSeries.Points.Clear();
            double firstX = points.First().XValue;
            double lastX=0;
            foreach (var point in points)
            {
                dataSeries.Points.Add(point);
                lastX = point.XValue;
            }
            var dataRange = lastX - firstX;
            var step = Math.Pow(10, Math.Floor(Math.Log10(dataRange)-.7));
            var axis = base.ChartAreas[0].AxisX;
            axis.Minimum = Math.Floor(firstX/step)*step;
            axis.Maximum = Math.Ceiling(lastX/step)*step;
            axis.Interval = step;

        }
    }
}
