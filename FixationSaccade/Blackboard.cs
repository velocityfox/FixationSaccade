using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FixationSaccade
{
    class Blackboard
    {
        public int Left { get; }
        public int Top { get; }
        public int Width { get; }
        public int Height { get; }
        public Label ReadingArea { get; }
        public int NumberOfSlides { get { return slides.Count(); } }
        public Rectangle DisplayArea { get { return new Rectangle(Left, Top, Width, Height); } }
        List<string> slides;
        int slideOnShow = -1;

        public Blackboard(int left,int top, int width, int height, Label readingArea)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            ReadingArea = readingArea;
            slides = new List<string>();
        }
        public void AddSlideFromFile(string filepath)
        {
            string text = System.IO.File.ReadAllText(filepath, Encoding.Default);
            slides.Add(text);
        }
        public void ShowNextSlide()
        {
            slideOnShow++;
            if (slideOnShow >= NumberOfSlides)
            {
                slideOnShow = 0;
            }
            Display();
        }
        public void ShowPreviousSlide()
        {
            slideOnShow--;
            if (slideOnShow <= 0)
            {
                slideOnShow = NumberOfSlides-1;
            }
            Display();
        }
        public void ShowSlide(int i)
        {
            slideOnShow = i;
            Display();
        }
        public void Display()
        {
            string text = slides[slideOnShow];
            ReadingArea.Text = text;
            ReadingArea.Left = Left;
            ReadingArea.Top = Top;
            ReadingArea.Width = this.Width;
            ReadingArea.Height = this.Height;
            ReadingArea.Visible = true;
        }
        public void HideSlides()
        {
            slideOnShow = -1;
            ReadingArea.Text = "";
            ReadingArea.Visible = false;
        }
    }
}
