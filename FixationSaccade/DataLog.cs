using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FixationSaccade
{
    internal class DataLog
    {
        public DataLog(string csvFilepath, bool isHeading=true)
        {
            if (!isHeading)
                throw new NotSupportedException("Can only open files with headings.");
            this.Filepath = csvFilepath;
        }

        public string Filepath { get; private set; }
    }
    public class Entry
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Timestamp { get; private set; }
        public Entry(double x, double y, double timestamp)
        {
            X = x; Y = y; Timestamp = timestamp;
        }
    }
    public class GazeEntry : Entry
    {
        public GazeEntry(double x, double y, double timestamp) : base(x, y, timestamp)
        {
        }
    }
    internal class GazeLog : DataLog
    {
        public List<GazeEntry> GazePoints { get; private set; }
        public GazeLog(string csvFilepath, bool isHeading = true) : base(csvFilepath, isHeading)
        {
            GazePoints = new List<GazeEntry>();
            using (StreamReader sr = File.OpenText(csvFilepath))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var cols = sr.ReadLine().Split(',').Select(Double.Parse).ToList();
                    GazePoints.Add(new GazeEntry(cols[0], cols[1], cols[2]));
                }
            }
        }
    }
    public class FixationEntry
    {
        public FixationEntry(IEnumerable<GazeEntry> gazeEntries)
        {
            this.GazeEntries = gazeEntries;
            var Xs = gazeEntries.Select(f => f.X);
            var Ys = gazeEntries.Select(f => f.Y);
            var Ts = gazeEntries.Select(f => f.Timestamp);
            this.X = Xs.Average();
            this.RangeX = Xs.Max() - Xs.Min();
            this.Y = Ys.Average();
            this.RangeY = Ys.Max() - Ys.Min();
            this.Timestamp = Ts.First();
            this.Duration = Ts.Last()-Ts.First();
        }

        public IEnumerable<GazeEntry> GazeEntries { get; private set; }
        public double Timestamp { get; private set; }
        public double Y { get; private set; }
        public double X { get; private set; }
        public double RangeX { get; private set; }
        public double Duration { get; private set; }
        public double RangeY { get; private set; }
    }
    internal class FixationLog : DataLog
    {
        public List<FixationEntry> Fixations { get; private set; }
        public FixationLog(string csvFilepath, bool isHeading = true) : base(csvFilepath, isHeading)
        {
            Fixations = new List<FixationEntry>();
            using (StreamReader sr = File.OpenText(csvFilepath))
            {
                sr.ReadLine();
                List<GazeEntry> gazeEntries = new List<GazeEntry>();
                while (!sr.EndOfStream)
                {
                    var colStrings = sr.ReadLine().Split(',').ToList();
                    var cols = colStrings.Skip(1).Select(Double.Parse).ToList();
                    if (colStrings[0] == "Begin")
                        gazeEntries = new List<GazeEntry>();
                    gazeEntries.Add(new GazeEntry(cols[0], cols[1], cols[2]));
                    if(colStrings[0]=="End")
                    {
                        Fixations.Add(new FixationEntry(gazeEntries));
                        gazeEntries = new List<GazeEntry>();
                    }
                }
            }
        }
    }
}
