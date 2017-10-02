using FixationSaccade.Components;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FixationSaccade
{
    partial class Timeline
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
            this.button1 = new System.Windows.Forms.Button();
            this.ExperimentId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSlidingWindowSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonApplyCalibration = new System.Windows.Forms.Button();
            this.textBoxScalingY = new System.Windows.Forms.TextBox();
            this.textBoxScalingX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.microChannel = new FixationSaccade.Components.Channel();
            this.movementChannel = new FixationSaccade.Components.Channel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.microChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movementChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Analyse Experiment";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AnalyseExperiment_Click);
            // 
            // ExperimentId
            // 
            this.ExperimentId.Location = new System.Drawing.Point(158, 209);
            this.ExperimentId.Name = "ExperimentId";
            this.ExperimentId.Size = new System.Drawing.Size(240, 29);
            this.ExperimentId.TabIndex = 2;
            this.ExperimentId.Text = "7_2017-09-06@11.16.35";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Experiment ID: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSlidingWindowSize);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonApplyCalibration);
            this.groupBox1.Controls.Add(this.textBoxScalingY);
            this.groupBox1.Controls.Add(this.textBoxScalingX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(617, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 364);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calibration Data";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // textBoxSlidingWindowSize
            // 
            this.textBoxSlidingWindowSize.Location = new System.Drawing.Point(193, 70);
            this.textBoxSlidingWindowSize.Name = "textBoxSlidingWindowSize";
            this.textBoxSlidingWindowSize.Size = new System.Drawing.Size(41, 29);
            this.textBoxSlidingWindowSize.TabIndex = 5;
            this.textBoxSlidingWindowSize.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sliding window size";
            // 
            // buttonApplyCalibration
            // 
            this.buttonApplyCalibration.Location = new System.Drawing.Point(271, 315);
            this.buttonApplyCalibration.Name = "buttonApplyCalibration";
            this.buttonApplyCalibration.Size = new System.Drawing.Size(117, 43);
            this.buttonApplyCalibration.TabIndex = 3;
            this.buttonApplyCalibration.Text = "Apply";
            this.buttonApplyCalibration.UseVisualStyleBackColor = true;
            this.buttonApplyCalibration.Click += new System.EventHandler(this.buttonApplyCalibration_Click);
            // 
            // textBoxScalingY
            // 
            this.textBoxScalingY.Location = new System.Drawing.Point(240, 27);
            this.textBoxScalingY.Name = "textBoxScalingY";
            this.textBoxScalingY.Size = new System.Drawing.Size(94, 29);
            this.textBoxScalingY.TabIndex = 2;
            // 
            // textBoxScalingX
            // 
            this.textBoxScalingX.Location = new System.Drawing.Point(140, 27);
            this.textBoxScalingX.Name = "textBoxScalingX";
            this.textBoxScalingX.Size = new System.Drawing.Size(94, 29);
            this.textBoxScalingX.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Scaling (X,Y)";
            // 
            // microChannel
            // 
            this.microChannel.BackColor = System.Drawing.Color.Black;
            this.microChannel.Location = new System.Drawing.Point(1017, 199);
            this.microChannel.Name = "microChannel";
            this.microChannel.Size = new System.Drawing.Size(1157, 143);
            this.microChannel.TabIndex = 6;
            // 
            // movementChannel
            // 
            this.movementChannel.BackColor = System.Drawing.Color.Black;
            this.movementChannel.Location = new System.Drawing.Point(12, 12);
            this.movementChannel.Name = "movementChannel";
            this.movementChannel.Size = new System.Drawing.Size(2162, 181);
            this.movementChannel.TabIndex = 0;
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2203, 575);
            this.Controls.Add(this.microChannel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExperimentId);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.movementChannel);
            this.Name = "Timeline";
            this.Text = "TimeLine";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.microChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movementChannel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Channel movementChannel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ExperimentId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxScalingX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxScalingY;
        private System.Windows.Forms.TextBox textBoxSlidingWindowSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonApplyCalibration;
        private Channel microChannel;
    }
}