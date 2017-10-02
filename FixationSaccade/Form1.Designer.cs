using System.Windows.Forms;

namespace FixationSaccade
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ReadingArea = new System.Windows.Forms.Label();
            this.ParticipantCodeBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Calibrate = new System.Windows.Forms.Button();
            this.ReadGazeTimer = new System.Windows.Forms.Timer(this.components);
            this.Dump = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PreviousSlide = new System.Windows.Forms.Button();
            this.NextSlide = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ClearTracking = new System.Windows.Forms.Button();
            this.CalibrationFileReplay = new System.Windows.Forms.TextBox();
            this.ShowTracking = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GazeFileReplay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.MouseTimer = new System.Windows.Forms.Timer(this.components);
            this.MouseTrack = new System.Windows.Forms.Button();
            this.TrackingPanel = new FixationSaccade.TransparentPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Begin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.RunExperiment_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "Dotty!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ReadingArea
            // 
            this.ReadingArea.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ReadingArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadingArea.Font = new System.Drawing.Font("Consolas", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReadingArea.Location = new System.Drawing.Point(875, 141);
            this.ReadingArea.Name = "ReadingArea";
            this.ReadingArea.Size = new System.Drawing.Size(385, 135);
            this.ReadingArea.TabIndex = 2;
            this.ReadingArea.Text = "Lorem Ipsum £123";
            this.ReadingArea.Visible = false;
            // 
            // ParticipantCodeBox
            // 
            this.ParticipantCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParticipantCodeBox.Location = new System.Drawing.Point(192, 45);
            this.ParticipantCodeBox.Name = "ParticipantCodeBox";
            this.ParticipantCodeBox.Size = new System.Drawing.Size(59, 29);
            this.ParticipantCodeBox.TabIndex = 4;
            this.ParticipantCodeBox.Text = "18";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(10, 101);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(240, 49);
            this.button4.TabIndex = 5;
            this.button4.Text = "Load Tobii";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.LoadTobii_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Participant Code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.ParticipantCodeBox);
            this.groupBox1.Controls.Add(this.Calibrate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(24, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 216);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Eyetracker";
            // 
            // Calibrate
            // 
            this.Calibrate.Location = new System.Drawing.Point(10, 156);
            this.Calibrate.Name = "Calibrate";
            this.Calibrate.Size = new System.Drawing.Size(240, 48);
            this.Calibrate.TabIndex = 10;
            this.Calibrate.Text = "Calibrate";
            this.Calibrate.UseVisualStyleBackColor = true;
            this.Calibrate.Click += new System.EventHandler(this.Calibrate_Click);
            // 
            // ReadGazeTimer
            // 
            this.ReadGazeTimer.Interval = 20;
            this.ReadGazeTimer.Tick += new System.EventHandler(this.ReadGazeTimer_Tick);
            // 
            // Dump
            // 
            this.Dump.Location = new System.Drawing.Point(20, 27);
            this.Dump.Name = "Dump";
            this.Dump.Size = new System.Drawing.Size(180, 52);
            this.Dump.TabIndex = 11;
            this.Dump.Text = "Dump Calibration";
            this.Dump.UseVisualStyleBackColor = true;
            this.Dump.Click += new System.EventHandler(this.Dump_Click);
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(20, 93);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(671, 250);
            this.Output.TabIndex = 12;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(506, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(185, 49);
            this.button5.TabIndex = 14;
            this.button5.Text = "Start Recording";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(10, 84);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(240, 53);
            this.button6.TabIndex = 15;
            this.button6.Text = "Stop Recording";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(10, 142);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(240, 48);
            this.button7.TabIndex = 16;
            this.button7.Text = "Save Data";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(24, 287);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 209);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Experiment";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PreviousSlide);
            this.groupBox3.Controls.Add(this.NextSlide);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(24, 526);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 96);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Slide Show";
            // 
            // PreviousSlide
            // 
            this.PreviousSlide.Location = new System.Drawing.Point(10, 36);
            this.PreviousSlide.Name = "PreviousSlide";
            this.PreviousSlide.Size = new System.Drawing.Size(83, 42);
            this.PreviousSlide.TabIndex = 0;
            this.PreviousSlide.Text = "<";
            this.PreviousSlide.UseVisualStyleBackColor = true;
            this.PreviousSlide.Click += new System.EventHandler(this.PreviousSlide_Click);
            // 
            // NextSlide
            // 
            this.NextSlide.Location = new System.Drawing.Point(168, 36);
            this.NextSlide.Name = "NextSlide";
            this.NextSlide.Size = new System.Drawing.Size(83, 42);
            this.NextSlide.TabIndex = 16;
            this.NextSlide.Text = ">";
            this.NextSlide.UseVisualStyleBackColor = true;
            this.NextSlide.Click += new System.EventHandler(this.NextSlide_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ClearTracking);
            this.groupBox4.Controls.Add(this.CalibrationFileReplay);
            this.groupBox4.Controls.Add(this.ShowTracking);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.GazeFileReplay);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(318, 35);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(270, 436);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Replay Data";
            // 
            // ClearTracking
            // 
            this.ClearTracking.Location = new System.Drawing.Point(10, 342);
            this.ClearTracking.Name = "ClearTracking";
            this.ClearTracking.Size = new System.Drawing.Size(240, 49);
            this.ClearTracking.TabIndex = 26;
            this.ClearTracking.Text = "Clear Tracking";
            this.ClearTracking.UseVisualStyleBackColor = true;
            this.ClearTracking.Click += new System.EventHandler(this.ClearTracking_Click);
            // 
            // CalibrationFileReplay
            // 
            this.CalibrationFileReplay.Location = new System.Drawing.Point(15, 188);
            this.CalibrationFileReplay.Name = "CalibrationFileReplay";
            this.CalibrationFileReplay.Size = new System.Drawing.Size(236, 35);
            this.CalibrationFileReplay.TabIndex = 24;
            this.CalibrationFileReplay.Text = "Logs/18_2017-09-04@19.38.09_calibration.csv";
            // 
            // ShowTracking
            // 
            this.ShowTracking.Location = new System.Drawing.Point(10, 287);
            this.ShowTracking.Name = "ShowTracking";
            this.ShowTracking.Size = new System.Drawing.Size(240, 49);
            this.ShowTracking.TabIndex = 25;
            this.ShowTracking.Text = "Show Tracking";
            this.ShowTracking.UseVisualStyleBackColor = true;
            this.ShowTracking.Click += new System.EventHandler(this.ShowTracking_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 29);
            this.label4.TabIndex = 23;
            this.label4.Text = "Calibration File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 29);
            this.label3.TabIndex = 22;
            this.label3.Text = "Gaze File:";
            // 
            // GazeFileReplay
            // 
            this.GazeFileReplay.Location = new System.Drawing.Point(15, 115);
            this.GazeFileReplay.Name = "GazeFileReplay";
            this.GazeFileReplay.Size = new System.Drawing.Size(236, 35);
            this.GazeFileReplay.TabIndex = 21;
            this.GazeFileReplay.Text = "Logs/18_2017-09-04@19.38.09_gaze.csv";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Participant Code";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 232);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(240, 49);
            this.button3.TabIndex = 5;
            this.button3.Text = "Replay";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Replay_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(192, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 29);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "18";
            // 
            // MouseTrack
            // 
            this.MouseTrack.Location = new System.Drawing.Point(306, 30);
            this.MouseTrack.Name = "MouseTrack";
            this.MouseTrack.Size = new System.Drawing.Size(99, 46);
            this.MouseTrack.TabIndex = 21;
            this.MouseTrack.Text = "Mouse";
            this.MouseTrack.UseVisualStyleBackColor = true;
            this.MouseTrack.Click += new System.EventHandler(this.MouseTrack_Click);
            // 
            // TrackingPanel
            // 
            this.TrackingPanel.Location = new System.Drawing.Point(1077, 381);
            this.TrackingPanel.Name = "TrackingPanel";
            this.TrackingPanel.Size = new System.Drawing.Size(183, 96);
            this.TrackingPanel.TabIndex = 19;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Dump);
            this.groupBox5.Controls.Add(this.MouseTrack);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.Output);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Location = new System.Drawing.Point(12, 1180);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(719, 374);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button8);
            this.groupBox6.Location = new System.Drawing.Point(24, 628);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(270, 406);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Areas of Interest";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(18, 28);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(125, 39);
            this.button8.TabIndex = 0;
            this.button8.Text = "First Word";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2463, 1566);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.TrackingPanel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReadingArea);
            this.Name = "Form1";
            this.Text = "eyeStimuli";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label ReadingArea;
        private System.Windows.Forms.TextBox ParticipantCodeBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer ReadGazeTimer;
        private System.Windows.Forms.Button Calibrate;
        private System.Windows.Forms.Button Dump;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button PreviousSlide;
        private System.Windows.Forms.Button NextSlide;
        private GroupBox groupBox4;
        private Label label3;
        private TextBox GazeFileReplay;
        private Label label2;
        private Button button3;
        private TextBox textBox1;
        internal TransparentPanel TrackingPanel;
        private Timer MouseTimer;
        private Button MouseTrack;
        private TextBox CalibrationFileReplay;
        private Label label4;
        private Button ShowTracking;
        private Button ClearTracking;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private Button button8;
    }
    class TransparentPanel: System.Windows.Forms.Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}

