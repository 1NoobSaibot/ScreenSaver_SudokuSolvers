using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenSaver_SudokuSolver
{
    class Animation
    {
        readonly Bitmap cnvs;
        readonly Graphics g;
        Random rnd = new Random();
        Task repainter;

        public Animation(int width, int height)
        {
            cnvs = new Bitmap(width, height);
            g = Graphics.FromImage(cnvs);
            g.Clear(Color.Black);
            repainter = new Task(repaint);
            repainter.Start();
        }

        internal Image getImage()
        {
            return cnvs;
        }

        void repaint()
        {
            Pen pen = new Pen(new SolidBrush(Color.Green));
            int x1, y1, x2, y2;
            x2 = rnd.Next() % 1920;
            y2 = rnd.Next() % 1080;
            do
            {
                x1 = x2;
                y1 = y2;
                x2 = rnd.Next() % 1920;
                y2 = rnd.Next() % 1080;
                g.FillRectangle(new SolidBrush(Color.FromArgb(5, 0, 0, 0)), 0, 0, 1920, 1080);
                g.DrawLine(pen, x1, y1, x2, y2);
                Thread.Sleep(15);
            } while (true);
            
        }
    }
}
