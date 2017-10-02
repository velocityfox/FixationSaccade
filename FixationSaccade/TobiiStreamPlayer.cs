using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeXFramework;
using System.Collections.Concurrent;
using System.IO;
using System.Windows.Forms;
using Tobii.EyeX.Framework;

namespace FixationSaccade
{
    class TobiiStreamPlayer
    {
        public delegate void GazePointCallback(object sender,GazePointEventArgs e);
        public delegate void FixationCallback(object sender, FixationEventArgs e);
        public delegate void EyePositionCallback(object sender, EyePositionEventArgs e);
        private event GazePointCallback GazePointEvent;
        private event FixationCallback FixationEvent;
        private event EyePositionCallback EyePositionEvent;
        private GazePointDataStream gazePointDataStream;
        private FixationDataStream fixationDataStream;
        private EyePositionDataStream eyePositionDataStream;
        private ConcurrentQueue<GazePointEventArgs> gazePoints;
        private ConcurrentQueue<FixationEventArgs> fixations;
        private ConcurrentQueue<EyePositionEventArgs> eyePositions;
        private volatile bool  isGazePointDataRecording;
        private volatile bool  isFixationDataRecording;
        private volatile bool  isEyePositionDataRecording;
        private Timer replayTimer;
        public enum StreamType
        {
            GazePoint,
            EyePosition,
            Fixation
        }
        private EyeXHost eyeXHost;
        public TobiiStreamPlayer()
        {
            eyeXHost = new EyeXHost();
            eyeXHost.Start();
            gazePoints = new ConcurrentQueue<GazePointEventArgs>();
            fixations = new ConcurrentQueue<FixationEventArgs>();
            eyePositions = new ConcurrentQueue<EyePositionEventArgs>();
        }
        public void StartGazeDataStreams(
            GazePointCallback gazePointCallback, string gazePointDataFilePath,
            FixationCallback fixationCallback, string fixationsDataFilePath,
            EyePositionCallback eyePositionCallback, string eyePositionDataFilePath)
        {
            isGazePointDataRecording = false;
            isFixationDataRecording = false;
            isEyePositionDataRecording = false;
            GazePointEvent = null;
            if (gazePointDataFilePath != null)
            {
                GazePointEvent += gazePointCallback;
                //Load gaze point data
                using (var sr = File.OpenText(gazePointDataFilePath))
                {
                    //Heading
                    var line = sr.ReadLine();
                    var columns = line.Split(',');
                    int gazeXIndex = columns.ToList().IndexOf("gazeX");
                    int gazeYIndex = columns.ToList().IndexOf("gazeY");
                    int gazeTimestampIndex = columns.ToList().IndexOf("gazeTimestamp");
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        var columnValues = line.Split(',').Select(x => Double.Parse(x)).ToArray(); ;
                        gazePoints.Enqueue(new GazePointEventArgs(columnValues[gazeXIndex], columnValues[gazeYIndex], columnValues[gazeTimestampIndex]));
                    }
                }
            }
            FixationEvent = null;
            if (fixationsDataFilePath != null)
            {
                FixationEvent += fixationCallback;
                //Load fixation  data
                using (var sr = File.OpenText(fixationsDataFilePath))
                {
                    //Heading
                    var line = sr.ReadLine();
                    var columns = line.Split(',');
                    int fixationEventTypeIndex = columns.ToList().IndexOf("fixationEventType");
                    int fixationXIndex = columns.ToList().IndexOf("fixationX");
                    int fixationYIndex = columns.ToList().IndexOf("fixationY");
                    int fixationTimestampIndex = columns.ToList().IndexOf("fixationTimestamp");
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        var columnValues = line.Split(',').ToArray();
                        Enum.TryParse(columnValues[fixationEventTypeIndex], out FixationDataEventType eventType);
                        fixations.Enqueue(new FixationEventArgs(eventType,
                            Double.Parse(columnValues[fixationXIndex]),
                            Double.Parse(columnValues[fixationYIndex]),
                            Double.Parse(columnValues[fixationTimestampIndex])
                            ));
                    }
                }
            }
            if (eyePositionDataFilePath != null)
            {
                EyePositionEvent += eyePositionCallback;
                //Load eye position  data
                using (var sr = File.OpenText(eyePositionDataFilePath))
                {
                    //Heading
                    var line = sr.ReadLine();
                    var columns = line.Split(',');
                    int leftIsValidIndex = columns.ToList().IndexOf("leftIsValid");
                    int leftNormalisedIsValidIndex = columns.ToList().IndexOf("leftNormalisedIsValid");
                    int rightIsValidIndex = columns.ToList().IndexOf("rightIsValid");
                    int rightNormalisedIsValidIndex = columns.ToList().IndexOf("rightNormalisedIsValid");
                    int eyePositionTimestampIndex = columns.ToList().IndexOf("eyePositionTimestamp");
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        var columnValues = line.Split(',').ToArray();
                        Func<int, EyePosition> ParseEyePositionBlock = i =>
                            new EyePosition(
                                Boolean.Parse(columnValues[i]),
                                Double.Parse(columnValues[i + 1]),
                                Double.Parse(columnValues[i + 2]),
                                Double.Parse(columnValues[i + 3])
                            );
                        eyePositions.Enqueue(new EyePositionEventArgs(
                            ParseEyePositionBlock(leftIsValidIndex),
                            ParseEyePositionBlock(leftNormalisedIsValidIndex),
                            ParseEyePositionBlock(rightIsValidIndex),
                            ParseEyePositionBlock(rightNormalisedIsValidIndex),
                            Double.Parse(columnValues[eyePositionTimestampIndex])
                            ));
                    }
                }
            }
            replayTimer = new Timer();
            replayTimer.Interval = 16;//ms
            replayTimer.Tick += (s, e) =>
              {
                  if (gazePoints.TryDequeue(out GazePointEventArgs gp))
                      GazePointEvent.Invoke(s, gp);
                  if (fixations.TryDequeue(out FixationEventArgs f))
                      FixationEvent.Invoke(s, f);
                  if (eyePositions.TryDequeue(out EyePositionEventArgs ep))
                      EyePositionEvent.Invoke(s, ep);
                  if (gazePoints.Count() == 0 && fixations.Count() == 0 && eyePositions.Count() == 0)
                  {
                      replayTimer.Stop();
                      replayTimer.Enabled = false;
                      replayTimer.Dispose();
                  }
              };
            replayTimer.Start();
            replayTimer.Enabled = true;
        }
        public void StartGazeDataStreams(GazePointCallback gazePointCallback, FixationCallback fixationCallback, EyePositionCallback eyePositionCallback)
        {
            if (gazePointCallback != null)
            {
                if (gazePointDataStream == null)
                {
                    gazePointDataStream = eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.Unfiltered);
                }
                gazePointDataStream.Next += InvokeGazePointEvent;
                GazePointEvent += gazePointCallback;
                GazePointEvent += RecordGazePoint;
            }
            if (fixationCallback != null)
            {
                if (fixationDataStream == null)
                    fixationDataStream = eyeXHost.CreateFixationDataStream(FixationDataMode.Sensitive);
                fixationDataStream.Next += InvokeFixationEvent;
                FixationEvent += fixationCallback;
                FixationEvent += RecordFixation;
            }
            if(eyePositionCallback!=null)
            {
                if (eyePositionDataStream == null)
                    eyePositionDataStream = eyeXHost.CreateEyePositionDataStream();
                eyePositionDataStream.Next += InvokeEyePositionEvent;
                EyePositionEvent += eyePositionCallback;
                EyePositionEvent += RecordEyePosition;
            }
        }
        private void RecordGazePoint(object s, GazePointEventArgs e)
        {
            if (isGazePointDataRecording)
                gazePoints.Enqueue(new GazePointEventArgs(e.X, e.Y, e.Timestamp));
        }
        private void RecordFixation(object s, FixationEventArgs e)
        {
            if (isFixationDataRecording)
                fixations.Enqueue(new FixationEventArgs(e.EventType, e.X, e.Y, e.Timestamp));
        }
        private void RecordEyePosition(object s, EyePositionEventArgs e)
        {
            Func<EyePosition, EyePosition> cloneEyePosition = ep =>
             new EyePosition(ep.IsValid, ep.X, ep.Y, ep.Z);
            if (isEyePositionDataRecording)
                eyePositions.Enqueue(new EyePositionEventArgs(
                    cloneEyePosition(e.LeftEye),
                    cloneEyePosition(e.LeftEyeNormalized),
                    cloneEyePosition(e.RightEye),
                    cloneEyePosition(e.RightEyeNormalized),
                    e.Timestamp
                    ));
        }
        private void InvokeGazePointEvent(object s,GazePointEventArgs e)
        {
            GazePointEvent.Invoke(s, e);
        }
        private void InvokeFixationEvent(object s, FixationEventArgs e)
        {
            FixationEvent.Invoke(s, e);
        }
        private void InvokeEyePositionEvent(object s, EyePositionEventArgs e)
        {
            EyePositionEvent.Invoke(s, e);
        }
        public void StartAllGazeDataRecording() { SetGazeDataRecording(true, true, true); }
        public void SetGazeDataRecording(bool isStartGazePoint, bool isStartFixation, bool isStartEyePosition)
        {
            isGazePointDataRecording = isStartGazePoint;
            isFixationDataRecording = isStartFixation;
            isEyePositionDataRecording = isStartEyePosition;
        }
        public void StopAllGazeDataRecording() { SetGazeDataRecording(false, false, false); }
        public void SaveAllGazeDataStreams(string gazePointFilepath, string fixationFilepath, string eyePositionFilepath)
        {
            if (isGazePointDataRecording || isFixationDataRecording || isEyePositionDataRecording)
                throw new Exception("Cannot save gaze data streams while recording.");
            using (var sw = File.CreateText(gazePointFilepath))
            {
                sw.WriteLine("gazeX,gazeY,gazeTimestamp");
                foreach (var gazePoint in gazePoints.ToList())
                    sw.WriteLine(String.Format("{0},{1},{2}", gazePoint.X, gazePoint.Y, gazePoint.Timestamp));
            }
            using (var sw = File.CreateText(fixationFilepath))
            {
                sw.WriteLine("fixationEventType,fixationX,fixationY,fixationTimestamp");
                foreach (var fixation in fixations.ToList())
                    sw.WriteLine(String.Format("{0},{1},{2},{3}", fixation.EventType, fixation.X, fixation.Y,fixation.Timestamp));
            }
            using (var sw = File.CreateText(eyePositionFilepath))
            {
                sw.WriteLine("leftIsValid,leftX,leftY,leftZ,leftNormalisedIsValid,leftNormalisedX,leftNormalisedY,leftNormalisedZ,rightIsValid,rightX,rightY,rightZ,rightNormalisedIsValid,rightNormalisedX,rightNormalisedY,rightNormalisedZ,eyePositionTimestamp");
                Func<EyePosition, string> eyePositionToString = e =>
                 String.Format("{0},{1},{2},{3}", e.IsValid, e.X, e.Y, e.Z);
                foreach (var eyePosition in eyePositions.ToList())
                    sw.WriteLine(String.Format("{0},{1},{2},{3},{4}", 
                       eyePositionToString(eyePosition.LeftEye),
                       eyePositionToString(eyePosition.LeftEyeNormalized),
                       eyePositionToString(eyePosition.RightEye),
                       eyePositionToString(eyePosition.RightEyeNormalized),
                       eyePosition.Timestamp
                       ));
            }
        }
        public void StopAllGazeDataStream()
        {
            SetGazeDataRecording(false, false, false);
            GazePointEvent = null;
            gazePointDataStream.Dispose();
            gazePointDataStream = null;
            FixationEvent = null;
            fixationDataStream.Dispose();
            fixationDataStream = null;
            EyePositionEvent = null;
            eyePositionDataStream.Dispose();
            eyePositionDataStream = null;
        }
        public void ClearAllGazeDataRecordings()
        {
            if (isGazePointDataRecording || isFixationDataRecording || isEyePositionDataRecording)
                throw new Exception("Cannot clear all gaze data while recording streams.");
            gazePoints = new ConcurrentQueue<GazePointEventArgs>();
            fixations = new ConcurrentQueue<FixationEventArgs>();
            eyePositions = new ConcurrentQueue<EyePositionEventArgs>();
        }
    }
}
