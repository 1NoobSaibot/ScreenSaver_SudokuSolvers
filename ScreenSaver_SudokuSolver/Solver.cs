﻿using SudokuLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenSaver_SudokuSolver
{
    class Solver
    {
        static Brush green = new SolidBrush(Color.Green);
        static Pen normal;
        static Pen bold;
        static Font font;
        static float dx = 8.5f;
        static float dy = 5.7f;
        static Random rnd = new Random();

        private int x;
        private int y;
        private Sudoku sudoku;
        private Task solver;
        
        public Solver(int x, int y)
        {
            this.x = x;
            this.y = y;
            sudoku = new Sudoku();
            if (normal == null)
            {
                normal = new Pen(new SolidBrush(Color.Green));
                bold = new Pen(new SolidBrush(Color.Green));
                bold.Width = 3;
                font = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                font = new Font("CourierNew", 11, FontStyle.Regular);
            }
            solver = new Task(solve);
            solver.Start();
        }

        public void draw(Graphics g)
        {
            int w = 30 * 9;
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), x, y, w, w);
            //Создание сетки
            g.DrawLine(bold, x, y, x + w, y);
            for (int Y = 0; Y < 3; Y++)
            {
                for (int _y = 1; _y < 3; _y++)
                    g.DrawLine(normal, x, y + (Y*3 + _y) * 30, x + w, y + (Y * 3 + _y) * 30);
                g.DrawLine(bold, x, y + (Y + 1) * 3 * 30, x + w, y + (Y + 1) * 3 * 30);
            }
            g.DrawLine(bold, x, y, x, y + w);
            for (int X = 0; X < 3; X++)
            {
                for (int _x = 1; _x < 3; _x++)
                    g.DrawLine(normal, x + (X * 3 + _x) * 30, y, x + (X * 3 + _x) * 30, y + w);
                g.DrawLine(bold, x + (X+1) * 3 * 30, y, x + (X+1) * 3 * 30, y + w);
            }

            for (int X = 0; X < 9; X++)
                for (int Y = 0; Y < 9; Y++)
                    g.DrawString(sudoku[X, Y].ToString(), font, green, x + dx + X * 30, y + dy + Y * 30);
                    
        }

        private void solve()
        {
            do
            {
                sudoku.initGame(0);
                Thread.Sleep(10);
            } while (true);
        }
    }
}
