using SudokuLib;
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
        static Brush black = new SolidBrush(Color.Black);
        static Pen normal;
        static Pen bold;
        static Font font;
        static float dx = 8.5f;
        static float dy = 5.7f;
        static RandomLocker rnd = new RandomLocker();
        public static int dbg_loopCounter = 0;

        private int x;
        private int y;
        private Sudoku sudoku;
        private Task solver;
        private bool GameOver;

        public Solver(int x, int y)
        {
            this.x = x;
            this.y = y;
            sudoku = new Sudoku();
            sudoku.OnSolved += OnGameSolved;
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
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), x - 10, y - 10, w + 20, w + 20);
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
                {
                    if (sudoku.isFixed(X, Y))
                    {
                        g.FillRectangle(green, x + 2 + X * 30, y + 2 + Y * 30, 26, 26);
                        g.DrawString(sudoku[X, Y].ToString(), font, black, x + dx + X * 30, y + dy + Y * 30);
                    }
                    else if (sudoku[X, Y] != 0) g.DrawString(sudoku[X, Y].ToString(), font, green, x + dx + X * 30, y + dy + Y * 30);
                }
                    
                    
        }


        private void OnGameSolved(Sudoku sender)
        {
            GameOver = true;
        }

        private void solve()
        {
            do
            {
                sudoku.initGame(50, rnd);
                GameOver = false;
                Thread.Sleep(100);
                do
                {
                    int x, y;
                    for (int i = 0; i < 20; i++)
                    {
                        dbg_loopCounter++;
                        x = rnd.Next() % 9;
                        y = rnd.Next() % 9;
                        if (sudoku[x, y] != 0) continue;
                        byte solution = sudoku.isOnlyOneSolution(x, y);
                        if (solution == 0) continue;
                        else
                        {
                            sudoku[x, y] = solution;
                            Thread.Sleep(1500);
                            break;
                        }
                    }
                    Thread.Sleep(500);
                } while (!GameOver);
                
            } while (true);
        }


    }

    
}
