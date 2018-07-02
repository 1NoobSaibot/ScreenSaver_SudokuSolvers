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
        Bitmap cnvs, buff;
        Graphics g, gbuf;
        Random rnd = new Random();
        Task repainter;

        public Animation(int width, int height)
        {
            cnvs = new Bitmap(width, height);
            buff = new Bitmap(width, height);
            g = Graphics.FromImage(cnvs);
            gbuf = Graphics.FromImage(buff);
            g.Clear(Color.Black);
            repainter = new Task(repaint);
            repainter.Start();
        }

        internal Image getImage()
        {
            return buff;
        }

        void repaint()
        {
            Solver solver = new Solver(20, 20);
            
            do
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(5, 0, 0, 0)), 0, 0, 1920, 1080);
                solver.draw(g);
                gbuf.DrawImage(cnvs, 0, 0);
                Thread.Sleep(15);
            } while (true);
            
        }
    }
}
