using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixationSaccade
{
    class MovementEntry
    {
        public MovementEntry(double timestamp,double distance)
        {
            this.Timestamp = timestamp;
            this.Distance = distance;
        }

        public double Timestamp { get; private set; }
        public double Distance { get; private set; }
    }
    public class ScalingVector
    {
        public ScalingVector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Y { get; private set; }
        public double X { get; private set; }
    }
    class GazeAnalyser
    {
        public List<MovementEntry> Movements {
            get {
                var movements = new List<MovementEntry>();
                for (int i = 1; i < GazePoints.Count; i++)
                {
                    var point = GazePoints[i];
                    var prev = GazePoints[i - 1];
                    movements.Add(new MovementEntry(
                        (point.Timestamp),
                        Math.Sqrt(
                            (point.X - prev.X) * (point.X - prev.X)
                            + (point.Y - prev.Y) * (point.Y - prev.Y)
                        )
                    ));
                }
                return movements;
            }
        }

        public event Action OnMovementDataChanged = null;
        public event Action<GlobalScalingLocalOffsetCalibrator> OnCalibratorChanged = null;
        public event Action OnFixationDataChanged = null;
        public event Action OnStableGazePointsChanged = null;
        public GlobalScalingLocalOffsetCalibrator Calibrator;
        private double startTime;
        private FixationLog fixationLog;

        public string LoadedExperimentId { get; private set; }
        public IList<GazeEntry> GazePoints { get; private set; }
        public int SlidingWindowSize { get; private set; }
        public IList<GazeEntry> StableGazePoints { get; private set; }
        public IList<FixationEntry> Fixations { get; private set; }

        public GazeAnalyser()
        {
            GazePoints = new List<GazeEntry>();
            StableGazePoints = new List<GazeEntry>();
            Fixations = new List<FixationEntry>();
        }
        public void LoadExperiment(string experimentId)
        {
            LoadedExperimentId = experimentId;
            var filepathStem = Path.Combine("Logs", LoadedExperimentId);
            var gazeLog = new GazeLog(filepathStem+"_gaze.csv");
            this.startTime = gazeLog.GazePoints[0].Timestamp;
            this.GazePoints = gazeLog.GazePoints.Select(p => new GazeEntry(p.X, p.Y, p.Timestamp - startTime)).ToList();
            this.Calibrator = new GlobalScalingLocalOffsetCalibrator(
                FixationSaccade.Calibrator.DeserialiseCalibrationMapping(
                    File.ReadAllText(Path.Combine("Logs", experimentId + "_calibration.csv"))
                )
            );
            this.fixationLog = new FixationLog(filepathStem + "_fixation.csv");
            this.Fixations = fixationLog.Fixations.Select(f => new FixationEntry(f.GazeEntries.Select(g => new GazeEntry(g.X,g.Y,g.Timestamp-startTime)))).ToList();
            OnCalibratorChanged.Invoke(Calibrator);
            OnFixationDataChanged.Invoke();
            OnMovementDataChanged.Invoke();
        }
        public void SetScalingVector(ScalingVector scalingVector)
        {
            Calibrator.SetScaling(scalingVector.X, scalingVector.Y);
            OnCalibratorChanged.Invoke(Calibrator);
        }
        public void SetSlidingWindowSize(int size)
        {
            this.SlidingWindowSize = size;
            this.StableGazePoints = new List<GazeEntry>();
            for (int i = 0; i < GazePoints.Count - SlidingWindowSize; i++)
            {
                double xSum=0d, ySum=0d;
                for (int j = 0; j < SlidingWindowSize; j++)
                {
                    xSum += GazePoints[i + j].X;
                    ySum += GazePoints[i + j].Y;
                }
                StableGazePoints.Add(new GazeEntry(xSum / ((double)SlidingWindowSize), ySum / ((double)SlidingWindowSize),GazePoints[i].Timestamp));
            }
            OnStableGazePointsChanged.Invoke();
        }
    }
}
