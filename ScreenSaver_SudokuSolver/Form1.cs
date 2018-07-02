using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver_SudokuSolver
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Animation animation;
        
        public Form1()
        {
            InitializeComponent();
            Size s = Screen.PrimaryScreen.Bounds.Size;
            
            animation = new Animation(s.Width, s.Height);
            cnvs.Height = s.Height;
            cnvs.Width = s.Width;
            cnvs.Image = animation.getImage();
            gr = Graphics.FromImage(cnvs.Image);
            refresher.Start();
        }
        
        private void refresh(object sender, EventArgs e)
        {
            cnvs.Image = animation.getImage();
            cnvs.Refresh();
        }
    }
}
