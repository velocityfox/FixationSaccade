using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FixationSaccade
{
    partial class Form1
    {
        private Graphics graphics;
        private List<PaintEventHandler> thingsToPaint = new List<PaintEventHandler>();
        private void ReadyPanel()
        {
            TrackingPanel.Width = this.ClientRectangle.Width;
            TrackingPanel.Height = this.ClientRectangle.Height;
            TrackingPanel.Top = this.ClientRectangle.Top;
            TrackingPanel.Left = this.ClientRectangle.Left;
        }
        public void DrawCircle(int x, int y, Brush colour = null)
        {
            DrawElipse(x, y, 30, 30, colour == null ? Brushes.SkyBlue : colour);
        }
        public void DrawElipse(int x, int y, int width, int height, Brush colour = null)  
        {
            ReadyPanel();
            Pen p = new Pen(colour == null ? Brushes.SpringGreen : colour);
            DrawThing((s, e) =>
             e.Graphics.DrawEllipse(p, new Rectangle(x - (int)((double)width)/2, y - (int)((double)height) /2, width, height))
              );

        }
        public void DrawSquare(int x, int y, Brush colour, int width)
        {
            int halfWidth = width / 2;
            ReadyPanel();
            DrawThing((s, e) =>
              e.Graphics.FillRectangle(colour, new Rectangle(x-halfWidth, y-halfWidth, width, width))
              );
        }
        public void DrawDot(int x, int y, Brush colour = null)
        {
            if (colour == null)
                colour = Brushes.Red;
            int px = 1;
            DrawSquare(x, y, colour, px);
        }
        public void ClearDrawings()
        {
            foreach (var thing in thingsToPaint)
            {
                TrackingPanel.Paint -= thing;
            }
            thingsToPaint.Clear();
            this.Refresh();
        }
        private void DrawThing(PaintEventHandler drawingLambda)
        {
            thingsToPaint.Add(drawingLambda);
            TrackingPanel.Paint += drawingLambda;
            TrackingPanel.Invalidate();
        }
    }
}
