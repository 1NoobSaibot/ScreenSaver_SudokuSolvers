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
        static int dbg_loopCounter = 0;

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
            Solver[] solvers = new Solver[30];
            for (int i = 0; i < solvers.Length; i++)
                solvers[i] = new Solver(rnd.Next() % (1920 - 270), rnd.Next() % (1080 - 270));

            Font font = new Font("Courier New", 20);
            Brush dbg = new SolidBrush(Color.Red);
            
            do
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 0, 0, 1920, 1080);
                for (int i = 0; i < solvers.Length; i++)
                    solvers[i].draw(g);
                g.DrawString("Loops Solve: " + Solver.dbg_loopCounter, font, dbg, 20, 20);
                g.DrawString("Loops Repaint: " + Animation.dbg_loopCounter, font, dbg, 20, 60);
                gbuf.DrawImage(cnvs, 0, 0);
                Thread.Sleep(15);
                dbg_loopCounter++;
            } while (true);
            
        }
    }
}
