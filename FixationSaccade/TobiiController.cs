using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeX.Framework;

namespace FixationSaccade
{
    public class TobiiController
    {
        GazePointEventArgs gaze = new GazePointEventArgs(0, 0, 0);
        FixationEventArgs fixation = new FixationEventArgs(FixationDataEventType.Begin, 0, 0, 0);
        EyePositionEventArgs eyePosition = new EyePositionEventArgs(
            new EyePosition(false, 0, 0, 0),
            new EyePosition(false, 0, 0, 0),
            new EyePosition(false, 0, 0, 0),
            new EyePosition(false, 0, 0, 0),
            0);
        StableGazePoint stableGaze;
        Calibrator calibrator;
        CalibratorBuilder calibratorBuilder;
        Timer calibrationTimer;
        TobiiStreamPlayer player;
        private readonly int gazeStabilityWindow;

        public TobiiController(int gazeStabilityWindow=20)
        {
            this.gazeStabilityWindow = gazeStabilityWindow;
            stableGaze = new StableGazePoint(gazeStabilityWindow);
            player = new TobiiStreamPlayer();
        }
        public void LoadTobiiProfile(string participantCode)
        {
            //Start tracking
            player.StartGazeDataStreams(GazeDataStream_Next,FixationDataStream_Next,EyePositionDataStream_Next);
        }
        public void LoadTobiiProfile(string participantCode, string calibrationDataFilepath, string gazeDataFilepath)
        {
            calibrator = new GlobalScalingLocalOffsetCalibrator(Calibrator.DeserialiseCalibrationMapping(File.ReadAllText(calibrationDataFilepath)));
            player.StartGazeDataStreams(GazeDataStream_Next, gazeDataFilepath, null, null, null, null);
        }
        public GazePointEventArgs GetGazePoint()
        {
            GazePointEventArgs gp;
            lock (gaze)
                gp = new GazePointEventArgs(gaze.X, gaze.Y, gaze.Timestamp);
            return gp;
        }
        public GazePointEventArgs GetStableGazePoint()
        {
            GazePointEventArgs gp;
            lock (stableGaze)
                gp = new GazePointEventArgs(stableGaze.X, stableGaze.Y, stableGaze.Timestamp);
            return gp;
        }
        public GazePointEventArgs GetCalibratedGazePoint()
        {
            var stableGazePoint =GetStableGazePoint();
            var calibratedPoint = calibrator.CalibrateGazePoint(new GazePoint(stableGaze.X, stableGaze.Y));
            return new GazePointEventArgs(calibratedPoint.X, calibratedPoint.Y, stableGazePoint.Timestamp);
        }
        public void StartCalibration(Rectangle calibrationArea,Form1 winForm)
        {
            calibratorBuilder = new CalibratorBuilder(
                calibrationArea,
                CalibratorBuilder.Strategy.Square9);
            calibrationTimer = new Timer();
            calibrationTimer.Interval = 20;
            calibrationTimer.Tick += (object s,EventArgs e)
                =>CalibrationTimer_Tick(s,e,winForm);
            calibrationTimer.Enabled = true;//redundant??
            calibrationTimer.Start();
        }
        private void CalibrationTimer_Tick(object sender, EventArgs e, Form1 winForm)
        {
            if (calibratorBuilder.Points.Where(p => calibratorBuilder.IsMoreCalibrationNeeded(p)).Any())
            {
                var point = calibratorBuilder.Points.Where(p => calibratorBuilder.IsMoreCalibrationNeeded(p)).First();
                int boxWidth = 4;
                winForm.DrawSquare((int)(point.X) - boxWidth / 2, (int)(point.Y) - boxWidth / 2, Brushes.Purple, boxWidth);
                calibratorBuilder.AddCalibration(point, GetGazePoint());
                if (!calibratorBuilder.IsMoreCalibrationNeeded(point))
                {
                    winForm.ClearDrawings();
                }
            }
            else
            {
                calibrationTimer.Stop();
                calibrationTimer.Enabled = false;
                calibrator = calibratorBuilder.BuildCalibratedScreen();
                MessageBox.Show("Calibration complete");
            }
        }
        public void SaveCalibrationData(string filepath)
        {
            File.WriteAllText(filepath, calibrator.ToString());
        }
        public void SaveGazeData(string gazePointFilepath,string fixationFilepath,string eyePositionFilepath)
        {
            player.SaveAllGazeDataStreams(gazePointFilepath,fixationFilepath,eyePositionFilepath);
        }

        public string DumpCalibration()
        {
            return calibrator.ToString();
        }
        public void StartGazeDataRecording() { player.StartAllGazeDataRecording(); }
        public void StopGazeDataRecording() { player.StopAllGazeDataRecording(); }


        private void GazeDataStream_Next(object sender, GazePointEventArgs e)
        {
            lock (gaze)
                gaze = new GazePointEventArgs(e.X, e.Y, e.Timestamp);
            lock (stableGaze)
                stableGaze.Add(new GazePointEventArgs(e.X, e.Y, e.Timestamp));
        }
        private void FixationDataStream_Next(object sender, FixationEventArgs e)
        {
            lock (fixation)
                fixation = e;
        }
        private void EyePositionDataStream_Next(object sender, EyePositionEventArgs e)
        {
            lock (eyePosition)
                eyePosition = e;
        }
    }
}
