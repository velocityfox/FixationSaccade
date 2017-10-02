using Analytics.Maths.Timeseries;
using EyeXFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FixationSaccade
{
    public partial class Form1 : Form
    {

        Blackboard blackboard;
        TobiiController tobii;

        public Timeline Timeline { get; private set; }

        public Form1()
        {
            InitializeComponent();
            //this.TrackingPanel.Click += (s, e) => { this.TrackingPanel.Visible = false; };
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            this.Width = (int)(2560.0/1.5);
            this.Height = (int)(1600.0/1.5) + this.Height-this.ClientRectangle.Height;
            graphics = CreateGraphics();
            blackboard = new Blackboard(
                (int)(this.Width * .30),    //Left
                (int)(this.ClientRectangle.Height * .05),   //Top
                (int)(this.Width * .40),    //Width
                (int)(this.ClientRectangle.Height * .90),    //Height
                ReadingArea
                );
            if (!Directory.Exists("Slides"))
            {
                MessageBox.Show("Cannot find the 'Slides' folder. Please copy it to this folder and restart the program.");
                Environment.Exit(0);
            }
            foreach (var file in Directory.EnumerateFiles("Slides"))
            {
                blackboard.AddSlideFromFile(file);
            }
            tobii = new TobiiController();
            //blackboard.ShowNextSlide();
            this.Timeline = new Timeline(this);
            this.Timeline.Show();
            this.Location = new System.Drawing.Point(0,0);
            this.Timeline.Location = new System.Drawing.Point(0,this.Height);
            Timeline.Width = this.Width;
        }
        private void LoadTobii_Click(object sender, EventArgs e)
        {
            tobii.LoadTobiiProfile(ParticipantCodeBox.Text);
            tobii.StartGazeDataRecording();
            MessageBox.Show("Loaded profile: " + ParticipantCodeBox.Text);
        }
        private void RunExperiment_Click(object sender, EventArgs e)
        {
            TrackingPanel.Width = this.Width;
            TrackingPanel.Height = this.ClientRectangle.Height;
            TrackingPanel.Top = this.Top;
            TrackingPanel.Left = this.Left;
            TrackingPanel.BringToFront();
            TrackingPanel.Enabled = true;
            ReadGazeTimer.Start();
            TrackingPanel.Visible = true;
            blackboard.ShowNextSlide();

        }
        private void ReadGazeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //var stableGaze = tobii.GetStableGazePoint();
                //Sophisticated Calibration
                var calibratedStableGazePoint = tobii.GetCalibratedGazePoint();
                DrawSquare((int)calibratedStableGazePoint.X, (int)calibratedStableGazePoint.Y, Brushes.Red, 2);
            }
            catch { }
        }
        private void Replay_Click(object sender, EventArgs e)
        {
            tobii.LoadTobiiProfile(ParticipantCodeBox.Text, CalibrationFileReplay.Text, GazeFileReplay.Text);
            ReadGazeTimer.Start();
        }

        private void Calibrate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Look at the purple boxes when they appear on the screen. Press SPACEBAR when you are ready to go.");
            blackboard.ReadingArea.Visible = false;
            tobii.StartCalibration(blackboard.DisplayArea,this);
        }

        private void Dump_Click(object sender, EventArgs e)
        {
            Output.Text = tobii.DumpCalibration();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            tobii.StartGazeDataRecording();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tobii.StopGazeDataRecording();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrackingPanel.Enabled = true;
            TrackingPanel.BringToFront();
        }


        private void Save_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("Logs");
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss");
            tobii.SaveCalibrationData(String.Format("Logs/{0}_{1}_calibration.csv", ParticipantCodeBox.Text, timestamp));
            tobii.SaveGazeData(
                String.Format("Logs/{0}_{1}_gaze.csv", ParticipantCodeBox.Text, timestamp),
                String.Format("Logs/{0}_{1}_fixation.csv", ParticipantCodeBox.Text, timestamp),
                String.Format("Logs/{0}_{1}_eyePosition.csv", ParticipantCodeBox.Text, timestamp)
                );
        }

        private void PreviousSlide_Click(object sender, EventArgs e)
        {
            blackboard.ShowPreviousSlide();
        }

        private void NextSlide_Click(object sender, EventArgs e)
        {
            blackboard.ShowNextSlide();
        }


        private void MouseTrack_Click(object sender, EventArgs e)
        {
            TrackingPanel.Width = this.Width;
            TrackingPanel.Height = this.ClientRectangle.Height;
            TrackingPanel.Top = this.Top;
            TrackingPanel.Left = this.Left;
            Output.Text = "qwe";
            MouseTimer.Tick += (s, ev) =>
            {
                Output.Text = (new Random()).Next(1000).ToString();
                DrawSquare(MousePosition.X, MousePosition.Y, Brushes.BlueViolet, 7);
            };
            TrackingPanel.Invalidate();
            MouseTimer.Start();
            MouseTimer.Enabled = true;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            TrackingPanel.Width = this.Width;
            TrackingPanel.Height = this.ClientRectangle.Height;
            TrackingPanel.Top = this.Top;
            TrackingPanel.Left = this.Left;
            DrawSquare(MousePosition.X, MousePosition.Y, Brushes.BlueViolet, 50);
        }

        private void ShowTracking_Click(object sender, EventArgs e)
        {
            TrackingPanel.Visible = true;
        }

        private void ClearTracking_Click(object sender, EventArgs e)
        {
            ClearDrawings();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ParticipantCodeBox.Text = "1";
        }
    }
}
